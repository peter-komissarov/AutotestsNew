using System.Collections.Generic;
using System.Threading.Tasks;
using TestBase.Clients.GitHub.Responses;
using TestBase.Helpers;

namespace TestBase.Clients.GitHub
{
    /// <summary>
    /// Реализация клиента Github.
    /// </summary>
    public class GitHubClient
    {
        private readonly string _branchesUri;

        public GitHubClient()
        {
            _branchesUri = AppSettingsProvider.Configuration["BaseUri:GitHub"] + "repos/aspnet/AspNetCore.Docs/branches";
        }

        /// <summary>
        /// Возвращает список веток GitHub.
        /// </summary>
        /// <param name="withLog">Требуется ли выводить в консоль http запрос и ответ.</param>
        public async ValueTask<IEnumerable<BranchResponse>> GetBranchesAsync(bool withLog = true)
        {
            using var client = new Client()
                .WithHeaders(Headers.GitHub)
                .WithLog(withLog)
                .WithUri(_branchesUri);

            var branches = await client
                .GetAsync<IEnumerable<BranchResponse>>()
                .ConfigureAwait(false);

            return branches;
        }
    }
}