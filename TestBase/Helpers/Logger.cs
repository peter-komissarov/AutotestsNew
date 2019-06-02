using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace TestBase.Helpers
{
    public static class Logger
    {
        public static void Request((HttpClient httpClient, HttpRequestMessage request) clientWithRequest, object content)
        {
            var fullUri = new UriBuilder(new Uri(new Uri(clientWithRequest.httpClient.BaseAddress.AbsoluteUri), clientWithRequest.request.RequestUri));
            var message = new StringBuilder($"Type: {clientWithRequest.request.Method.Method}{Environment.NewLine}URI: {fullUri.Uri}");

            if (content != null)
            {
                message.Append($"{Environment.NewLine}{JsonConvert.SerializeObject(content, Formatting.Indented)}");
            }

            Message(message.ToString());
        }

        public static void Response(object poco)
        {
            Message($"Response: {JsonConvert.SerializeObject(poco, Formatting.Indented)}");
        }

        public static void Message(string messageText)
        {
            Console.WriteLine($"{DateTime.UtcNow}{Environment.NewLine}{messageText}{Environment.NewLine}");
        }

        public static void Message<T>(string description, T obj)
        {
            var formattedDescription = $"{DateTime.UtcNow}{Environment.NewLine}{description}";
            var message = JsonConvert.SerializeObject(obj, Formatting.Indented);

            Console.WriteLine($"{formattedDescription}{Environment.NewLine}{message}{Environment.NewLine}");
        }
    }
}
