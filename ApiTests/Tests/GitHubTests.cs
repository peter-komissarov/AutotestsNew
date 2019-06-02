using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestBase.Services;

namespace ApiTests.Tests
{
    [TestClass]
    public class GitHubTests : BaseApiTest
    {
        [TestMethod]
        public async Task GetBranches_Positive()
        {
            var gitHubService = _container.GetService<GitHubService>();
            var branched = await gitHubService.GetBranchesAsync().ConfigureAwait(false);

            Assert.IsTrue(branched.Count() >= 30);
        }
    }
}