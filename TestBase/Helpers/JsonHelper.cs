using Newtonsoft.Json;

namespace TestBase.Helpers
{
    /// <summary>
    /// Provides help methods for JSON format
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Returns string from JSON object 
        /// </summary>
        /// <param name="value">JSON object</param>
        public static string SerializeJson(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
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
    }
}