using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestBase.DataBase.Tables;
using TestBase.RestApi.Services;

namespace ApiTests.Tests
{
    /// <summary>
    /// GitHub tests
    /// </summary>
    [TestClass]
    public class GitHubTests : BaseApiTest
    {
        [TestMethod]
        [Description("Assert that github branches count more than 30")]
        public async Task GetBranches_Positive()
        {
            var invoice = await new InvoiceTable()
                .GetByInvoiceIdAsync(1068)
                .ConfigureAwait(false);
            Assert.IsTrue(invoice.OperationGuid == new Guid("D117A65E-FD2E-4767-91A6-ED3528A3D206"));

            var branches = await new GitHubService()
                .GetBranchesAsync()
                .ConfigureAwait(false);
            Assert.IsTrue(branches.Count() >= 30, $"Github branches count should be equal or more than 30, but founded {branches.Count()}");
        }
    }
}