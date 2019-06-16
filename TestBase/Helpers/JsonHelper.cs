using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace TestBase.Helpers
{
    /// <summary>
    /// Предоставляет вспомогательные методы для работы с форматом JSON.
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Преобразует объект в string content.
        /// </summary>
        /// <param name="content">Http content в виде анонимного объекта.</param>
        public static StringContent ObjectToStringContent(object content)
        {
            var json = ObjectToString(content);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Преобразует строку в POCO класс.
        /// </summary>
        /// <typeparam name="T">POCO класс.</typeparam>
        /// <param name="value">Строка в формате JSON.</param>
        public static T StringToPoco<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Преобразует объект в строку.
        /// </summary>
        /// <param name="value">JSON object.</param>
        public static string ObjectToString(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }
    }
}