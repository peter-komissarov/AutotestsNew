using NUnit.Framework;

namespace TestBase
{
    public abstract class BaseTest
    {
        /// <summary>
        /// Выполняется один раз перед запуском всех тестов в прогоне.
        /// </summary>
        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
        }

        /// <summary>
        /// Запускается перед запуском каждого теста.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
        }

        /// <summary>
        /// Запускается после каждого теста.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
        }

        /// <summary>
        /// Запускается один раз после выполнения всех тестов в прогоне.
        /// </summary>
        [OneTimeTearDown]
        public virtual void OneTimeTearDown()
        {
        }
    }
}
