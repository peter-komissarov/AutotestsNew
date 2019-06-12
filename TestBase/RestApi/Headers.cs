using System.Collections.Generic;

namespace TestBase.RestApi
{
    /// <summary>
    /// Http request headers.
    /// </summary>
    public static class Headers
    {
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