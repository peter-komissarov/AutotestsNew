using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestBase.Helpers
{
    /// <summary>
    /// Предоставляет вспомогательные методы для работы с форматом JSON.
    /// </summary>
    public static class JsonProvider
    {
        /// <summary>
        /// Преобразует JSON String в POCO класс.
        /// </summary>
        /// <typeparam name="T">POCO класс.</typeparam>
        /// <param name="value">Строка в формате JSON.</param>
        public static T Deserialize<T>(string value)
        {
            T poco = default;

            try
            {
                poco = JsonConvert.DeserializeObject<T>(value);
            }
            catch
            {
                Assert.Fail(
                    "Couldn't deserialize http response"
                    + $"{Environment.NewLine}"
                    + $"{Serialize(JObject.Parse(value))}"
                    + $"{Environment.NewLine}");
            }

            return poco;
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