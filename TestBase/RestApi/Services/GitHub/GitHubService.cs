using System.Collections.Generic;
using System.Threading.Tasks;
using TestBase.RestApi.Services.GitHub.Responses;

namespace TestBase.RestApi.Services.GitHub
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
            using (var client = new RestApiClient(GitHubUrls.GitHubBranches))
            {
                var branches = await client
                    .WithHeaders(Headers.GitHubHeaders)
                    .GetAsync<IEnumerable<BranchResponse>>(withLog)
                    .ConfigureAwait(false);

                return branches;
            }
        }
    }
}