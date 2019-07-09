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
        private static readonly string _branchesUri;
        private static readonly Dictionary<string, string> _gitHubHeaders;

        static GitHubClient()
        {
            _branchesUri = $"{AppSettingsProvider.Configuration["BaseUri:GitHub"]}repos/aspnet/AspNetCore.Docs/branches";
            _gitHubHeaders = new Dictionary<string, string>
            {
                {"Accept", "application/vnd.github.v3+json"},
                {"Accept-Language", AppSettingsProvider.Configuration["Format:Language"]},
                {"User-Agent", "HttpClientFactory-Sample"}
            };
        }

        /// <summary>
        /// Возвращает список веток GitHub.
        /// </summary>
        /// <param name="withLog">Требуется ли выводить в консоль http запрос и ответ.</param>
        public async ValueTask<IEnumerable<BranchResponse>> GetBranchesAsync(bool withLog = true)
        {
            using var client = new Client()
                .WithHeaders(_gitHubHeaders)
                .WithLog(withLog)
                .WithUri(_branchesUri);

            var branches = await client
                .GetAsync<IEnumerable<BranchResponse>>()
                .ConfigureAwait(false);

            return branches;
        }
    }
}