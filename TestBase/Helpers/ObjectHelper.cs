using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace TestBase.Helpers
{
    /// <summary>
    /// Provides help methods for object type
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// Returns StringContent from object content
        /// </summary>
        /// <param name="content">Http content</param>
        public static StringContent BuildStringContent(object content)
        {
            var json = JsonConvert.SerializeObject(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
