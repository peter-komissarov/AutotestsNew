using System.Collections.Generic;

namespace TestBase.RestApi.Headers
{
    /// <summary>
    /// Http request headers
    /// </summary>
    public static class RequestHeaders
    {
        public static Dictionary<string, string> GitHubHeaders => new Dictionary<string, string>
            {{"Accept", "application/vnd.github.v3+json"}, {"User-Agent", "HttpClientFactory-Sample"}};
    }
}