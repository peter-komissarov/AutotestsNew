using System.Collections.Generic;

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
        public static Dictionary<string, string> GitHubHeaders;

        static Headers()
        {
            GitHubHeaders = new Dictionary<string, string>
            {
                {"Accept", "application/vnd.github.v3+json"},
                {"Accept-Language", Configuration.Language},
                {"User-Agent", "HttpClientFactory-Sample"}
            };
        }
    }
}