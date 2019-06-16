using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    [TestClass]
    public class SmokeTests : BaseApiTest
    {
        private static IEnumerable<object[]> GetData()
        {
            yield return new object[]
            {
                new BranchResponse
                {
                    Name = "RP-vs-MVC/ra",
                    Commit = new Commit
                    {
                        Sha = "4d8264c44c39ef92e6b632edcef3cc74adb8925d",
                        Url = new Uri("https://api.github.com/repos/aspnet/AspNetCore.Docs/commits/4d8264c44c39ef92e6b632edcef3cc74adb8925d")
                    },
                    Protected = false
                }
            };

            yield return new object[]
            {
                new BranchResponse
                {
                    Name = "2/ODATA/security/ra",
                    Commit = new Commit
                    {
                        Sha = "b6473109fb38d38a20415ee0a27e74b4ac57bc06",
                        Url = new Uri("https://api.github.com/repos/aspnet/AspNetCore.Docs/commits/b6473109fb38d38a20415ee0a27e74b4ac57bc06")
                    },
                    Protected = false
                }
            };
        }

        [Description("Проверяет, что количество веток GitHub равняется 30.")]
        [TestMethod]
        public async Task Check_BranchesCount_Positive()
        {
            var invoice = await new InvoicesTable()
                .GetByUserIdAsync(new Guid("01C4D55C-B94D-473B-B4FE-B84CC6F77DC3"))
                .ConfigureAwait(false);

            var branchesCount = (await new GitHubService()
                    .GetBranchesAsync()
                    .ConfigureAwait(false))
                .Count();

            LogHelper.WriteText("Проверка InvoiceId...");
            Assert.AreEqual(870, invoice.InvoiceId, $"Фактический InvoiceId равняется {invoice.InvoiceId}, а ожидалось 870.");
            LogHelper.WriteText("Успешно!!!");

            LogHelper.WriteText("Проверка количества веток GitHub...");
            Assert.AreEqual(30, branchesCount, $"Фактическое количество веток GitHub равняется {branchesCount}, а ожидалось 30.");
            LogHelper.WriteText("Успешно!!!");
        }

        [Description("Проверяет, что в коллекции веток GitHub присутствует ожидаемая ветка.")]
        [DynamicData(nameof(GetData), DynamicDataSourceType.Method)]
        [TestMethod]
        public async Task Check_BranchesCollection_Positive(object expectedBranch)
        {
            var branches = await new GitHubService()
                .GetBranchesAsync()
                .ConfigureAwait(false);

            var branchesCollection = branches.Select(JsonHelper.ObjectToString).ToArray();
            LogHelper.WriteValue("Фактическая коллекция веток", branches);
            LogHelper.WriteValue("Ожидаемая ветка", expectedBranch);

            LogHelper.WriteText("Проверка коллекции веток GitHub...");
            CollectionAssert.Contains(
                branchesCollection,
                JsonHelper.ObjectToString(expectedBranch),
                "В фактической коллекции веток GitHub отсутствует ожидаемая ветка.");
            LogHelper.WriteText("Успешно!!!");
        }
    }
}