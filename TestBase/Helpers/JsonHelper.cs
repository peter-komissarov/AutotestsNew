using Newtonsoft.Json;

namespace TestBase.Helpers
{
    /// <summary>
    /// Предоставляет вспомогательные методы для работы с форматом JSON.
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Преобразует JSON String в POCO класс.
        /// </summary>
        /// <typeparam name="T">POCO класс.</typeparam>
        /// <param name="value">Строка в формате JSON.</param>
        public static T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Преобразует объект в JSON Formatted String.
        /// </summary>
        /// <param name="value">JSON object.</param>
        public static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }
    }
}