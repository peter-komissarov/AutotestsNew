using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestBase.IoC;

namespace ApiTests
{
    [TestClass]
    public abstract class BaseApiTest
    {
        protected static IServiceProvider _container;

        /// <summary>
        /// Executes once before the test run (Optional).
        /// </summary>
        /// <param name="context">TestContext class. This class should be fully abstract and not contain any members. The adapter will implement the members.</param>
        /// <returns>A new <see cref="T:System.Threading.Tasks.Task" /> with the specified action.</returns>
        [AssemblyInitialize]
        public static async Task TestRunSetup(TestContext context)
        {
            _container = await IocContainer.GetContainerAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Executes once for the test class (Optional).
        /// </summary>
        /// <param name="context">TestContext class. This class should be fully abstract and not contain any members. The adapter will implement the members.</param>
        [ClassInitialize]
        public static void TestClassSetup(TestContext context)
        {
        }

        /// <summary>
        /// Executes before each test (Optional).
        /// </summary>
        [TestInitialize]
        public void TestMethodSetup()
        {
        }

        /// <summary>
        /// Executes after each test (Optional).
        /// </summary>
        [TestCleanup]
        public void TestMethodCleanup()
        {
        }

        /// <summary>
        /// Executes once after all tests in this class are executed (Optional).
        /// </summary>
        [ClassCleanup]
        public static void TestClassCleanup()
        {
        }

        /// <summary>
        /// Executes once after the test run (Optional).
        /// </summary>
        [AssemblyCleanup]
        public static void TestRunCleanup()
        {
        }
    }
}