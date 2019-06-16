using System.Collections.Generic;
using TestBase.Helpers;

namespace TestBase.RestApi
{
    /// <summary>
    /// Headers для http запросов.
    /// </summary>
    public static class Headers
    {
        /// <summary>
        /// Headers для http запросов к сервису GitHub.
        /// </summary>
        public static readonly Dictionary<string, string> GitHub;

        static Headers()
        {
            GitHub = new Dictionary<string, string>
            {
                {"Accept", "application/vnd.github.v3+json"},
                {"Accept-Language", ConfigurationHelper.TestConfig["Culture&Format:Language"]},
                {"User-Agent", "HttpClientFactory-Sample"}
            };
        }
    }
}