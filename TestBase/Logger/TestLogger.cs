using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using TestBase.Helpers;

namespace TestBase.Logger
{
    /// <summary>
    /// Console logger implementation
    /// </summary>
    public static class TestLogger
    {
        /// <summary>
        /// Write to console http request message
        /// </summary>
        /// <param name="request"></param>
        public static void LogRequest(HttpRequestMessage request)
        {
            var message = new StringBuilder($"Type: {request.Method}{Environment.NewLine}URI: {request.RequestUri}");

            if (request.Headers != null)
            {
                message.Append($"{Environment.NewLine}Headers:");
                request.Headers.ToList().ForEach(header => message.Append($"{Environment.NewLine}{JsonHelper.SerializeJson(header)}"));
            }

            if (request.Content != null)
            {
                message.Append($"{Environment.NewLine}{JsonHelper.SerializeJson(request.Content)}");
            }

            LogMessage(message.ToString());
        }

        /// <summary>
        /// Write to console http response message
        /// </summary>
        /// <param name="poco"></param>
        public static void LogResponse(object poco)
        {
            LogMessage($"Response:{Environment.NewLine}{JsonHelper.SerializeJson(poco)}");
        }

        /// <summary>
        /// Write to console text message
        /// </summary>
        /// <param name="messageText"></param>
        public static void LogMessage(string messageText)
        {
            Console.WriteLine($"{DateTimeHelper.GetCurrentDateTime()}{Environment.NewLine}{messageText}{Environment.NewLine}");
        }
    }
}
