using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestBase.Clients.GitHub;
using TestBase.DataBase.Tables;
using TestBase.Helpers;

namespace ApiTests.GithubTests
{
    /// <summary>
    /// Тесты сервиса GitHub.
    /// </summary>
    [TestClass]
    public class SmokeTests : BaseApiTest
    {
        private static IEnumerable<object[]> GetData()
        {
            yield return new object[]
            {
                new
                {
                    Name = "RP-vs-MVC/ra",
                    Commit = new
                    {
                        Sha = "4d8264c44c39ef92e6b632edcef3cc74adb8925d",
                        Url = new Uri("https://api.github.com/repos/aspnet/AspNetCore.Docs/commits/4d8264c44c39ef92e6b632edcef3cc74adb8925d")
                    },
                    Protected = false
                }
            };

            yield return new object[]
            {
                new
                {
                    Name = "2/ODATA/security/ra",
                    Commit = new
                    {
                        Sha = "b6473109fb38d38a20415ee0a27e74b4ac57bc06",
                        Url = new Uri("https://api.github.com/repos/aspnet/AspNetCore.Docs/commits/b6473109fb38d38a20415ee0a27e74b4ac57bc06")
                    },
                    Protected = false
                }
            };
        }

        [Description("Проверяет, что в коллекции веток GitHub присутствует ожидаемая ветка.")]
        [DataTestMethod]
        [DynamicData(nameof(GetData), DynamicDataSourceType.Method)]
        public async Task Positive_CheckBranchesCollection_Async(object expectedBranch)
        {
            var branches = await new GitHubClient()
                .GetBranchesAsync()
                .ConfigureAwait(false);

            var branchesCollection = branches
                .Select(JsonProvider.Serialize)
                .ToArray();

            CollectionAssert.Contains(
                branchesCollection,
                JsonProvider.Serialize(expectedBranch),
                "Actual GitHub brunches collection does not contain an expected brunch.");
        }

        [Description("Проверяет, что количество веток GitHub равняется 30.")]
        [TestMethod]
        public async Task Positive_CheckBranchesCount_Async()
        {
            // тестирование запроса к базе данных
            var invoice = await new InvoicesTable()
                .GetByUserIdAsync(new Guid("01C4D55C-B94D-473B-B4FE-B84CC6F77DC3"))
                .ConfigureAwait(false);

            var branches = await new GitHubClient()
                .GetBranchesAsync()
                .ConfigureAwait(false);

            Assert.AreEqual(870, invoice.InvoiceId, $"Actual InvoiceId is {invoice.InvoiceId}, but 870 expected.");
            Assert.AreEqual(30, branches.Count(), $"Actual GitHub brunches count is {branches.Count()}, but 30 expected.");
        }
    }
}