using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace TestBase.Helpers
{
    /// <summary>
    /// Provides help methods for strings type
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Build absolute Uri with params
        /// </summary>
        /// <param name="uri">Uri without params</param>
        /// <param name="parameters">Request params</param>
        public static string BuildStringUri(string uri, object parameters)
        {
            var uriBuilder = new UriBuilder(uri);
            var queryParams = parameters.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .AsParallel()
                .AsUnordered()
                .Where(prop => prop.GetValue(parameters, null) != null)
                .Select(
                    prop =>
                        prop.Name + "=" +
                        Uri.EscapeDataString(
                            string.Format(
                                CultureInfo.InvariantCulture,
                                "{0}",
                                prop.GetValue(parameters, null))));

            if (!string.IsNullOrEmpty(uriBuilder.Query) && uriBuilder.Query.Length > 1)
            {
                uriBuilder.Query = uriBuilder.Query.Substring(1) + "&" + string.Join("&", queryParams);
            }
            else
            {
                uriBuilder.Query = string.Join("&", queryParams);
            }

            return uriBuilder.ToString();
        }
    }
}