using System;
using System.Text;

namespace TestBase.Helpers
{
    /// <summary>
    ///Предоставляет вспомогательные методы для преобразования строк.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Возвращает абсолютный Uri для Get и Delete http запросов.
        /// </summary>
        /// <param name="uri">Uri без параметров.</param>
        /// <param name="parameters">Параметры для добавления в Uri.</param>
        public static string GetAbsoluteUri(string uri, object parameters)
        {
            var stringBuilder = new StringBuilder();
            var uriBuilder = new UriBuilder(uri);

            var type = parameters.GetType();
            var propertyInfos = type.GetProperties();

            foreach (var propertyInfo in propertyInfos)
            {
                stringBuilder.Append($"{propertyInfo.Name}={propertyInfo.GetValue(parameters, null)}&");
            }

            uriBuilder.Query = stringBuilder.Remove(stringBuilder.Length - 1, 1).ToString();

            return uriBuilder.ToString();
        }
    }
}