using System;
using System.Text;

namespace TestBase.Helpers
{
    /// <summary>
    /// Provides help methods for strings type.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Build absolute Uri with params.
        /// </summary>
        /// <param name="uri">Uri without params.</param>
        /// <param name="parameters">Request params.</param>
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