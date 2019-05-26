using Newtonsoft.Json.Linq;
using System.IO;

namespace UnitTests
{
    public class TestsDataProvider
    {
        public static string GetConnectionString(){
            JToken jsonObj = JObject.Parse(File.ReadAllText("../../../../WebApp/appsettings.json"));
            string connection = (string)jsonObj.SelectToken("ConnectionStrings").SelectToken("DefaultConnection");

            return connection;
        }
    }
}