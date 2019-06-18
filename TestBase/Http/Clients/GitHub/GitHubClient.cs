using System.Collections.Generic;
using System.Threading.Tasks;
using TestBase.Helpers;
using TestBase.Http.Clients.GitHub.Responses;

namespace TestBase.Http.Clients.GitHub
{
    /// <summary>
    /// Реализация клиента Github.
    /// </summary>
    public class GitHubClient
    {
        private readonly string _branchesUri;

        public GitHubClient()
        {
            _branchesUri = ConfigHelper.Config["HostNames:GitHub"] + "repos/aspnet/AspNetCore.Docs/branches";
        }

        /// <summary>
        /// Возвращает список веток GitHub.
        /// </summary>
        /// <param name="withLog">Требуется ли выводить в консоль http запрос и ответ.</param>
        public async Task<IEnumerable<BranchResponse>> GetBranchesAsync(bool withLog = true)
        {
            using (var client = new Client())
            {
                var branches = await client
                    .WithHeaders(Headers.GitHub)
                    .WithUri(_branchesUri)
                    .GetAsync<IEnumerable<BranchResponse>>(withLog)
                    .ConfigureAwait(false);

                return branches;
            }
        }
    }
}