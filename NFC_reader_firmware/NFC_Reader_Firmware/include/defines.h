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

// NTP server settings
#define NTP_SERVER      "europe.pool.ntp.org"
#define GTM_OFFSET      7200
#define GTM_DAY_OFFSET  0

// API settings
// #define API_ENDPOINT    "http://ptsv2.com/t/je1wo-1552248829/post"
#define API_ENDPOINT  "http://lankomumas.duckdns.org:4430:/api/nfcscan/postscan"
//#define API_ENDPOINT    "http://192.168.0.101:4430/api/nfcscan/postscan"