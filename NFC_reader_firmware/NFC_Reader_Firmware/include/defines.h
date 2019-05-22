// SPI Pins
#define PN532_SCK       (18)
#define PN532_MOSI      (23)
#define PN532_SS        (5)
#define PN532_MISO      (19)

// Speaker pin to notify about succesfull scan
#define SPEAKER         (25)

// WiFi settings
#define SSID            "DESKTOP-A4GI4HB"
#define PASSWD          "bigbang123"
// #define SSID               "Redmi111"
// #define PASSWD             "abcabc123"
// #define SSID               "OnePlus 5"
// #define PASSWD             "inzinerija"

// Eduroam wifi settings
#define EAP_ANONYMOUS_IDENTITY  "anonymous@ktu.lt"
#define EAP_IDENTITY "vilval6@ktu.lt" //if connecting from another corporation, use identity@organisation.domain in Eduroam 
#define EAP_PASSWORD "BigBang032" //your Eduroam password
#define EAP_SSID  "eduroam" // Eduroam SSID
#define EAP_HOST  "" //external server domain for HTTP connection after authentification

// NTP server settings
#define NTP_SERVER      "europe.pool.ntp.org"
#define GTM_OFFSET      7200
#define GTM_DAY_OFFSET  3600

// API settings
// #define API_ENDPOINT     "http://ptsv2.com/t/je1wo-1552248829/post"
//#define API_ENDPOINT      "http://lankomumas.duckdns.org:4430:/api/nfcscan/postscan"
// #define API_ENDPOINT        "http://lankomumas.duckdns.org:4430/api/nfcscan/postscan"
// #define API_ENDPOINT_POLL   "http://lankomumas.duckdns.org:4430/api/nfcscan/getdeviceid/"
// #define API_ENDPOINT_UP_DEV "http://lankomumas.duckdns.org:4430/api/nfcscan/postdevice/"
#define API_ENDPOINT        "http://192.168.0.101:4430/api/nfcscan/postscan"
#define API_ENDPOINT_POLL   "http://192.168.0.101:4430/api/nfcscan/getdeviceid/"
#define API_ENDPOINT_UP_DEV "http://192.168.0.101:4430/api/nfcscan/postdevice/"
//#define API_ENDPOINT      "http://192.168.43.119:4430/api/nfcscan/postscan"

// Device config
#define EEPROM_SIZE 16
#define CHECK_UPDATE_INTER 10000