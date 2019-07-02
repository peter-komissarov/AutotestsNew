using System.Net.Http;
using System.Text;

namespace TestBase.Helpers
{
    /// <summary>
    /// Предоставляет вспомогательные методы для работы с типом Object.
    /// </summary>
    public static class ObjectProvider
    {
        /// <summary>
        /// Преобразует объект в string content.
        /// </summary>
        /// <param name="content">Http content в виде анонимного объекта.</param>
        public static StringContent ToStringContent(object content)
        {
            var json = JsonProvider.Serialize(content);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}