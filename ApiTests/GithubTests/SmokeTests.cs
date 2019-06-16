using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TestBase.Data.Poco;
using TestBase.Helpers;
using TestBase.Repositories.Tables;
using TestBase.RestApi.Services.GitHub;
using TestBase.RestApi.Services.GitHub.Responses;

namespace ApiTests.GithubTests
{
    /// <summary>
    /// Тесты сервиса GitHub.
    /// </summary>
    [TestFixture]
    public class SmokeTests : BaseApiTest
    {
        private static IEnumerable<BranchResponse> GetData()
        {
            yield return
                new BranchResponse
                {
                    Name = "RP-vs-MVC/ra",
                    Commit = new Commit
                    {
                        Sha = "4d8264c44c39ef92e6b632edcef3cc74adb8925d",
                        Url = new Uri("https://api.github.com/repos/aspnet/AspNetCore.Docs/commits/4d8264c44c39ef92e6b632edcef3cc74adb8925d")
                    },
                    Protected = false
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

        [Description("Проверяет, что количество веток GitHub равняется 30.")]
        [Test]
        public async Task Check_BranchesCount_Positive()
        {
            var invoice = await new InvoicesTable()
                .GetByUserIdAsync(new Guid("01C4D55C-B94D-473B-B4FE-B84CC6F77DC3"))
                .ConfigureAwait(false);

            var branchesCount = (await new GitHubService()
                    .GetBranchesAsync()
                    .ConfigureAwait(false))
                .Count();

            Assert.AreEqual(870, invoice.InvoiceId, $"Actual InvoiceId is {invoice.InvoiceId}, but 870 expected.");
            Assert.AreEqual(30, branchesCount, $"Actual GitHub brunches count is {branchesCount}, but 30 expected.");
        }

        [Description("Проверяет, что в коллекции веток GitHub присутствует ожидаемая ветка.")]
        [Test]
        [TestCaseSource(nameof(GetData))]
        public async Task Check_BranchesCollection_Positive(BranchResponse expectedBranch)
        {
            var branches = await new GitHubService()
                .GetBranchesAsync()
                .ConfigureAwait(false);

            var branchesCollection = branches.Select(JsonHelper.ObjectToString).ToArray();
            CollectionAssert.Contains(
                branchesCollection,
                JsonHelper.ObjectToString(expectedBranch),
                "Actual GitHub brunches collection does not contain an expected brunch.");
        }
    }
}