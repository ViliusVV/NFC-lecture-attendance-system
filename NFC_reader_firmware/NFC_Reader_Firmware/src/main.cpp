#include <Arduino.h> // Arduino framework library
#include <Wire.h>
#include <SPI.h>
#include <Adafruit_PN532.h> 
#include <WiFi.h>
#include "esp_wpa2.h" //wpa2 library for connections to Enterprise networks
#include "time.h"
#include <HTTPClient.h>
#include "defines.h"
#include "prototypes.h"
#include <ArduinoJson.h>
#include "EEPROM.h"

#define DEBUG
//#define EDUROAM


// NFC reader object, uses pins defined in defines.h
Adafruit_PN532 nfc(PN532_SCK, PN532_MISO, PN532_MOSI, PN532_SS);
uint64_t start;
uint64_t startHearbeat;
bool againLine = false;


// Functional Global variables
String deviceId = "9999";

void setup(void) {
  // Setup pins
  pinMode(SPEAKER, OUTPUT);
  
  // Setups peripherals
  setupSerial();
  setupNFCReader();
  wifiConnect();
  configureTime();

  // Load config
  
  if(!EEPROM.begin(EEPROM_SIZE))
  {
    Serial.println("Failed to init EEPROM!!");
  }
  EEPROM.commit();
  for (int i = 0; i < EEPROM_SIZE; i++)
  {
    Serial.print(byte(EEPROM.read(i))); Serial.print(" ");
  }
  readIdEPROM();

  // Setup time
  start = millis();
  startHearbeat = millis();
  // Nofify about finished setup
  Serial.printf("CPU:%d Heap:%d PsRam:%d\n", ESP.getCpuFreqMHz(), ESP.getHeapSize(), ESP.getPsramSize());
  beep(2, 100);
}

void loop(void) {
  bool success = false;
  uint8_t uid[] = { 0, 0, 0, 0, 0, 0, 0 };	// Buffer to store the returned UID
  uint8_t uidLength;				                // Length of the UID (4 or 7 bytes depending on ISO14443A card type)
  
  // Wait for an ISO14443A type cards
  // 'uid' will be populated with the UID, and uidLength will indicate
  // if the uid is 4 bytes (Mifare Classic) or 7 bytes (Mifare Ultralight)
  if(millis() - startHearbeat >= 500)
  {
    startHearbeat = millis();
    success = nfc.readPassiveTargetID(PN532_MIFARE_ISO14443A, &uid[0], &uidLength);
    parserNFCResults(success, uid, uidLength);
    printHearBeat();
  }
  if(millis() - start >= 3000)
  {
    againLine = true;
    start = millis();
    pollSettingsGet();
  }

  if((WiFi.status() == WL_CONNECTED) && success) 
  {
    String json;
    uint32_t cardUid = uid[0] + (uid[1] << 8) + (uid[2] << 16) + (uid[3] << 24);
    // time_t timestamp = getCurrentTimestamp();
    StaticJsonDocument<200> doc;
    char timeString[32] = "";
    printLocalTime(timeString);
    doc["UID"] = cardUid;
    doc["DeviceID"] = deviceId;
    doc["TimeStamp"] = timeString;
    serializeJson(doc, json);
    serializeJsonPretty(doc, Serial);
    Serial.println();
    sendPOSTRequest(json);
  }
}


void printHearBeat()
{
  if(againLine) { Serial.println(); againLine = false;}
  Serial.printf("%d, %d\n", ESP.getFreeHeap(), ESP.getMinFreeHeap());
}


void charToStringL(const char S[], String &D)
{
    byte at = 0;
    const char *p = S;
    D = "";

    while (*p++) {
      D.concat(S[at++]);
      }
}

// shows time in unix formtat
time_t getCurrentTimestamp()
{
  struct tm timeinfo;
  if(!getLocalTime(&timeinfo)){
    Serial.println("Failed to obtain time");
    return 0;
  }
  return mktime(&timeinfo);
}

bool pollSettingsGet()
{
  HTTPClient http;
  String json;
  String endpoint = API_ENDPOINT_POLL + deviceId;
  Serial.println(endpoint);

  // Configure traget server url
  Serial.print("[HTTP] Poll begin...\n");
  http.begin(endpoint);
  http.setTimeout(3500);

  // start connection and send HTTP header
  Serial.print("[HTTP] Poll GET...\n");
  int httpCode = http.GET();

  // HttpCode will be negative on error
  if(httpCode > 0) {
    // HTTP header has been send and Server response header has been handled
    Serial.printf("[HTTP]Poll GET... code: %d\n", httpCode);
    if(httpCode == HTTP_CODE_OK || httpCode == HTTP_CODE_CREATED) 
    {
      String payload = http.getString();
      //Serial.println(payload);
      StaticJsonDocument<200> doc;
      deserializeJson(doc, payload);
      serializeJsonPretty(doc, Serial);
      
      String deviceIdReal = doc["deviceIdReal"];
      String pendingDeviceId = doc["pendingDeviceId"];

      if(deviceIdReal != pendingDeviceId)
      {
        doc["deviceIdReal"] = pendingDeviceId;
        Serial.println("Changed id");
        serializeJsonPretty(doc, Serial);
        serializeJsonPretty(doc, json);


        HTTPClient httpUp;
        Serial.print("[HTTP] Up begin...\n");
        httpUp.begin(API_ENDPOINT_UP_DEV); 
        httpUp.addHeader("Content-Type", "application/json"); 
        Serial.print("[HTTP] Up POST...\n");
        httpCode = httpUp.POST(json);
        
        if(httpCode > 0) 
        {
          Serial.printf("[HTTP] Up POST... code: %d\n", httpCode);
          if(httpCode == HTTP_CODE_OK || httpCode == HTTP_CODE_CREATED) {
              payload = httpUp.getString();
              Serial.println(payload);
              deviceId = pendingDeviceId;
              updateEPROM();
          }
          else
          {
            beep(3, 50);
            http.end();
            return false;
          }
        } 
        else 
        {
          Serial.printf("[HTTP] Up POST... failed, error: %s\n", http.errorToString(httpCode).c_str());
          beep(4, 100);
          http.end();
          return false;
        }
        httpUp.end();
      }
      else
      {
        Serial.println("Id didnt change");
      }
    }
    else
    {
      beep(3, 50);
      http.end();
      return false;
    }
  } 
  else 
  {
    Serial.printf("[HTTP] Poll GET... failed, error: %s\n", http.errorToString(httpCode).c_str());
    beep(4, 100);
    http.end();
    return false;
  }
}

// Sends HTTP POST request to API endpoint
bool sendPOSTRequest(String messageJson)
{
  HTTPClient http;

  // Configure traget server url
  Serial.print("[HTTP] begin...\n");
  http.begin(API_ENDPOINT); 
  http.setTimeout(3500);
  http.addHeader("Content-Type", "application/json");

  // start connection and send HTTP header
  Serial.print("[HTTP] POST...\n");
  int httpCode = http.POST(messageJson);

  // HttpCode will be negative on error
  if(httpCode > 0) {
    // HTTP header has been send and Server response header has been handled
    Serial.printf("[HTTP] POST... code: %d\n", httpCode);

    // file found at server
    if(httpCode == HTTP_CODE_OK || httpCode == HTTP_CODE_CREATED) {
        String payload = http.getString();
        Serial.println(payload);
    }
    else
    {
      beep(3, 50);
    }
    http.end();
    return true;
  } 
  else 
  {
    Serial.printf("[HTTP] POST... failed, error: %s\n", http.errorToString(httpCode).c_str());
    beep(4, 100);
    http.end();
    return false;
  }
}

// Prints out results of NFC module
uint parserNFCResults(bool success, uint8_t (&uid)[7], uint8_t uidLength)
{
  if (success) {
    #ifdef DEBUG
    Serial.print("UID Length: ");Serial.print(uidLength, DEC);Serial.println(" bytes");
    Serial.print("UID Value: ");
    for (uint8_t i=0; i < uidLength; i++) 
    {
      Serial.print(" 0x");Serial.print(uid[i], HEX); 
    }
    Serial.println("");
    #endif
	// Wait 0.5 second before continuing
  beep(1,50);
  delay(100);
  
  return 1;
  }
  else
  {
    // PN532 probably timed out waiting for a card
    Serial.println(".");
    //beep(3,250);
    return 0;
  }
}


// Inicialises and gets time
void configureTime()
{
  configTime(GTM_OFFSET, GTM_DAY_OFFSET, NTP_SERVER);
  //printLocalTime();
}


// Print time in human readable format
bool printLocalTime(char s[])
{
  struct tm timeinfo;
  if(!getLocalTime(&timeinfo)){
    Serial.println("Failed to obtain time");
    return false;
  }
  //Serial.println(&timeinfo, "%A, %B %d %Y %H:%M:%S");
  //strftime(s,32,"%A, %B %d %Y %H:%M:%S", &timeinfo);
  strftime(s,32,"%Y-%m-%d %H:%M:%S", &timeinfo);
  //Serial.println(timeStr);
  return true;
}

void readIdEPROM()
{
  int id = EEPROM.readInt(4);
  Serial.println(id);
  deviceId = String(id);
}

void updateEPROM()
{
  int id = deviceId.toInt();
  EEPROM.writeInt(4,id);
  EEPROM.commit();
}


void wifiConnect()
{
  WiFi.disconnect(true);  //disconnect form wifi to set new wifi connection
  WiFi.mode(WIFI_STA); //init wifi mode
  

  #ifdef EDUROAM
    Serial.printf("Connecting to %s ", EAP_SSID);
    // esp_wifi_sta_wpa2_ent_set_identity((uint8_t *)EAP_ANONYMOUS_IDENTITY, strlen(EAP_IDENTITY)); //provide identity
    esp_wifi_sta_wpa2_ent_set_username((uint8_t *)EAP_IDENTITY, strlen(EAP_IDENTITY)); //provide username --> identity and username is same
    esp_wifi_sta_wpa2_ent_set_password((uint8_t *)EAP_PASSWORD, strlen(EAP_PASSWORD)); //provide password
    esp_wpa2_config_t config = WPA2_CONFIG_INIT_DEFAULT(); //set config settings to default
    esp_wifi_sta_wpa2_ent_enable(&config); //set config settings to enable function
    WiFi.begin(EAP_SSID);
  #else
    Serial.printf("Connecting to %s ", SSID);
    WiFi.begin(SSID, PASSWD);
  #endif // EDUROAM

  while (WiFi.status() != WL_CONNECTED) {
      delay(250);
      Serial.print(".");
  }
  Serial.println("\nCONNECTED");
  Serial.println(WiFi.localIP());
}


// Sets up and configures nfc reader module
void setupNFCReader()
{
  // Setup NFC module
  nfc.begin();

  // Get info
  uint32_t versiondata = nfc.getFirmwareVersion();
  if (! versiondata) {
    Serial.print("Didn't find PN53x board");
    while (1); // halt
  }
  
  // Got ok data, print it out!
  #ifdef DEBUG
  Serial.print("Found chip PN5"); Serial.println((versiondata>>24) & 0xFF, HEX); 
  Serial.print("PN532 Firmware ver. "); Serial.print((versiondata>>16) & 0xFF, DEC); 
  Serial.print('.'); Serial.println((versiondata>>8) & 0xFF, DEC);
  #endif

  // Set the max number of retry attempts to read from a card
  // This prevents us from waiting forever for a card, which is
  // the default behaviour of the PN532.
  nfc.setPassiveActivationRetries(0x05);
  
  // configure board to read RFID cards
  nfc.SAMConfig();
  
  Serial.println("Waiting for an RFID card");
}


void setupSerial()
{
  Serial.begin(115200);
  Serial.println("Startup!");
}


// Beep for notification purposes
void beep(int count, int duration)
{
  for(int i = 0; i < count; i++)
  {
    digitalWrite(SPEAKER, HIGH);
    delay(duration);
    digitalWrite(SPEAKER, LOW);
    if (i != count -1) // Last beep must not have pause
    {
      delay(duration);
    }
    
  }
  
}
