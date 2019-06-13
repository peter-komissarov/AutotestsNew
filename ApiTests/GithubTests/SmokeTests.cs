using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestBase.Data.PocoClasses;
using TestBase.Helpers;
using TestBase.Repositories.Tables;
using TestBase.RestApi.Services.GitHub;
using TestBase.RestApi.Services.GitHub.Responses;

namespace ApiTests.GithubTests
{
    /// <summary>
    /// Тесты сервиса GitHub.
    /// </summary>
    [TestClass]
    public class SmokeTests : BaseApiTest
    {
            public static IEnumerable<BranchResponse> GetData()
            {
                yield return new BranchResponse
                {
                    Name = RandomHelper.NextString(10),
                    Commit = new Commit
                    {
                        Sha = RandomHelper.NextString(5),
                        Url = new Uri(GitHubUri.GitHubBranches)
                    },
                    Protected = true
                };
                yield return new BranchResponse
                {
                    Name = "2/ODATA/security/ra",
                    Commit = new Commit
                    {
                        Sha = "b6473109fb38d38a20415ee0a27e74b4ac57bc06",
                        Url = new Uri("https://api.github.com/repos/aspnet/AspNetCore.Docs/commits/b6473109fb38d38a20415ee0a27e74b4ac57bc06")
                    },
                    Protected = false
                };
            }

        [Description("Проверяет, что количество веток GitHub больше или равно 30.")]
        [TestMethod]
        public async Task Check_BranchesCount_Positive()
        {
            var invoice = await new InvoicesTable()
                .GetByUserIdAsync(new Guid("01C4D55C-B94D-473B-B4FE-B84CC6F77DC3"))
                .ConfigureAwait(false);
            Assert.IsTrue(invoice.InvoiceId == 870, $"Ожидаемый InvoiceId = 870, фактический = {invoice.InvoiceId}.");

            var branches = await new GitHubService()
                .GetBranchesAsync()
                .ConfigureAwait(false);
            Assert.IsTrue(branches.Count() >= 30, $"Количество веток GitHub должно быть больше или равно 30, но фактически равно {branches.Count()}.");
        }

        [Description("Проверяет, что Ответ")]
        [DynamicData(nameof(GetData), DynamicDataSourceType.Method)]
        [TestMethod]
        public async Task Check_BranchesCollection_Contains_ExpectedBranch(BranchResponse expectedBranch)
        {
            var branches = await new GitHubService()
                .GetBranchesAsync()
                .ConfigureAwait(false);
            LogHelper.WriteValue("Ожидаемая ветка", expectedBranch);
            LogHelper.WriteValue("Фактическая коллекция веток", branches);
            CollectionAssert.Contains((ICollection)branches, expectedBranch, "Ожидаемая ветка не входит в коллекцию фактических веток");
        }
    }
}