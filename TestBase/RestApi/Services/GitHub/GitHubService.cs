using System.Collections.Generic;
using System.Threading.Tasks;
using TestBase.RestApi.Services.GitHub.Responses;

namespace TestBase.RestApi.Services.GitHub
{
    /// <summary>
    /// Реализация сервиса Github для авто-тестирования.
    /// </summary>
    public class GitHubService
    {
        /// <summary>
        /// Возвращает список веток GitHub.
        /// </summary>
        /// <param name="withLog">Требуется ли выводить в консоль http запрос и ответ.</param>
        public async Task<IEnumerable<BranchResponse>> GetBranchesAsync(bool withLog = true)
        {
            using (var client = new RestApiClient(GitHubUri.GitHubBranches))
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