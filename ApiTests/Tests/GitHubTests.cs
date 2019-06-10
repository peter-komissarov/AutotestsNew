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
        [Description("Assert that github branches count equal or more than 30")]
        public async Task GetBranches_Positive()
        {
            var invoice = await new InvoiceTable()
                .GetInvoiceByUserIdAsync(new Guid("01C4D55C-B94D-473B-B4FE-B84CC6F77DC3"))
                .ConfigureAwait(false);
            Assert.IsTrue(invoice.InvoiceId == 870, $"Expected InvoiceId is 870, but founded {invoice.InvoiceId}");

            var branches = await new GitHubService()
                .GetBranchesAsync()
                .ConfigureAwait(false);
            Assert.IsTrue(branches.Count() >= 30, $"Github branches count should be equal or more than 30, but founded {branches.Count()}");
        }
    }
}