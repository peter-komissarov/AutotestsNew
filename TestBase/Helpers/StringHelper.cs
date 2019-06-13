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
        public static string BuildStringUri(string uri, object parameters)
        {
            var uriBuilder = new UriBuilder(uri);
            var uriQuery = new StringBuilder();

            var type = parameters.GetType();
            var propertyInfos = type.GetProperties();

            foreach (var propertyInfo in propertyInfos)
            {
                uriQuery.Append($"{propertyInfo.Name}={propertyInfo.GetValue(parameters, null)}&");
            }

            uriBuilder.Query = uriQuery.Remove(uriQuery.Length - 1, 1).ToString();
            return uriBuilder.ToString();
        }
    }
}