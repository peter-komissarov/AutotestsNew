using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<BranchResponse>> GetBranchesAsync()
        {
            using (var client = new Client(GitHubUrl.GitHubBranches))
            {
                var branches = await client
                    .WithHeaders(Headers.GitHubHeaders)
                    .GetAsync<IEnumerable<BranchResponse>>()
                    .ConfigureAwait(false);

                return branches;
            }
        }
    }
}
