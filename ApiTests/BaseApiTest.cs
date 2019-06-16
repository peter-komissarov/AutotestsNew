using NUnit.Framework;

namespace ApiTests
{
    /// <summary>
    /// Базовый класс для API авто-тестов
    /// </summary>
    [Parallelizable(ParallelScope.All)]
    public abstract class BaseApiTest
    {
        /// <summary>
        /// Выполняется один раз перед запуском всех тестов в прогоне.
        /// </summary>
        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
        }

        /// <summary>
        /// Запускается перед запуском каждого теста.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
        }

        /// <summary>
        /// Запускается после каждого теста.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
        }

        /// <summary>
        /// Запускается один раз после выполнения всех тестов в прогоне.
        /// </summary>
        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
        }
    }
}