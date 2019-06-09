using System.Collections.Generic;
using System.Threading.Tasks;
using TestBase.RestApi.Headers;
using TestBase.RestApi.Response;
using TestBase.RestApi.Url;

namespace TestBase.RestApi.Services
{
    /// <summary>
    /// GitHub service implementation
    /// </summary>
    public class GitHubService
    {
        /// <summary>
        /// Returns GitHub branches list
        /// </summary>
        /// <param name="withLog">Log request and response or not?</param>
        public async Task<IEnumerable<BranchResponse>> GetBranchesAsync(bool withLog = true)
        {
            using (var client = new RestApiClient(GitHubUrl.GitHubBranches))
            {
                var branches = await client
                    .WithHeaders(RequestHeaders.GitHubHeaders)
                    .GetAsync<IEnumerable<BranchResponse>>(withLog)
                    .ConfigureAwait(false);

                return branches;
            }
        }
    }
}