using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace TestBase.Helpers
{
    /// <summary>
    /// Provides help methods for JSON format
    /// </summary>
    public static class JsonHelper
    {

        /// <summary>
        /// Returns StringContent from object content
        /// </summary>
        /// <param name="content">Http content</param>
        public static StringContent BuildStringContent(object content)
        {
            var json = SerializeJson(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Returns string from JSON object 
        /// </summary>
        /// <typeparam name="T">Type of poco</typeparam>
        /// <param name="value">JSON string</param>
        public static T DeserializeJson<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Returns string from JSON object 
        /// </summary>
        /// <param name="value">JSON object</param>
        public static string SerializeJson(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }
    }
}