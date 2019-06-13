using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestBase.Repositories.Tables;
using TestBase.RestApi.Services.GitHub;

namespace ApiTests.GithubTests
{
    /// <summary>
    /// Тесты сервиса GitHub.
    /// </summary>
    [TestClass]
    public sealed class SmokeTests : BaseApiTest
    {
        [Description("Проверяет, что количество веток GitHub больше или равно 30.")]
        [TestMethod]
        public async Task GetBranches_Positive()
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
    }
}