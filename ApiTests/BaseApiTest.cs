using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTests
{
    [TestClass]
    public abstract class BaseApiTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Запускается один раз перед выполнением всех тестов в прогоне.
        /// </summary>
        /// <param name="context">TestContext класс. Этот класс должен быть полностью абстрактным и не содержать ни каких членов класса. MSTest адаптер реализует члены этого класса самостоятельно.</param>
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
        }

        /// <summary>
        /// Запускается один раз перед выполнением всех тестов в тестовом классе.
        /// </summary>
        /// <param name="context">TestContext класс. Этот класс должен быть полностью абстрактным и не содержать ни каких членов класса. MSTest адаптер реализует члены этого класса самостоятельно.</param>
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
        }

        /// <summary>
        /// Запускается перед запуском каждого теста.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
        }

        /// <summary>
        /// Запускается после каждого теста.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
        }

        /// <summary>
        /// Запускается один раз после выполнения всех тестов в тестовом классе.
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        /// <summary>
        /// Запускается один раз после выполнения всех тестов в прогоне.
        /// </summary>
        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
        }
    }
}