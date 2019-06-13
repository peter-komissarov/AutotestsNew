using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTests
{
    [TestClass]
    public abstract class BaseApiTest
    {
        /// <summary>
        /// Запускается один раз перед выполнением всех тестов в прогоне.
        /// </summary>
        /// <param name="context">TestContext класс. Этот класс должен быть полностью абстрактным и не содержать ни каких членов класса. MSTest адаптер реализует члены этого класса самостоятельно.</param>
        [AssemblyInitialize]
        public static void TestRunSetup(TestContext context)
        {
        }

        /// <summary>
        /// Запускается один раз перед выполнением всех тестов в тестовом классе.
        /// </summary>
        /// <param name="context">TestContext класс. Этот класс должен быть полностью абстрактным и не содержать ни каких членов класса. MSTest адаптер реализует члены этого класса самостоятельно.</param>
        [ClassInitialize]
        public static void TestClassSetup(TestContext context)
        {
        }

        /// <summary>
        /// Запускается перед запуском каждого теста.
        /// </summary>
        [TestInitialize]
        public void TestMethodSetup()
        {
        }

        /// <summary>
        /// Запускается после каждого теста.
        /// </summary>
        [TestCleanup]
        public void TestMethodCleanup()
        {
        }

        /// <summary>
        /// Запускается один раз после выполнения всех тестов в тестовом классе.
        /// </summary>
        [ClassCleanup]
        public static void TestClassCleanup()
        {
        }

        /// <summary>
        /// Запускается один раз после выполнения всех тестов в прогоне.
        /// </summary>
        [AssemblyCleanup]
        public static void TestRunCleanup()
        {
        }
    }
}