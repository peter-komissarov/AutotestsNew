using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TestBase.Data.Responses;
using TestBase.HttpClients;

namespace TestBase.Services
{
    /// <summary>
    /// GitHub service implementation
    /// </summary>
    public class GitHubService
    {
        private const string BranchesRequestUri = "repos/aspnet/AspNetCore.Docs/branches";

        private readonly IHttpClientFactory _clientFactory;

        public GitHubService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private static IEnumerable<KeyValuePair<string, string>> RequestHeaders => new[]
        {
            new KeyValuePair<string, string>("Accept", "application/vnd.github.v3+json"),
            new KeyValuePair<string, string>("User-Agent", "HttpClientFactory-Sample")
        };

        /// <summary>
        /// Returns GitHub branches list
        /// </summary>
        public async Task<IEnumerable<BranchResponse>> GetBranchesAsync()
        {
            return await _clientFactory
                .CreateClient("github")
                .CreateRequest()
                .WithRequestUri(BranchesRequestUri)
                .WithHeaders(RequestHeaders)
                .GetAsync()
                .ReceiveJsonAsync<IEnumerable<BranchResponse>>()
                .ConfigureAwait(false);
        }
    }
}