using System;
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
        /// <param name="request">HttpRequestMessage</param>
        public static void LogRequest(HttpRequestMessage request)
        {
            var message = new StringBuilder($"Type: {request.Method}{Environment.NewLine}URI: {request.RequestUri}");

            if (request.Headers != null)
            {
                message.Append($"{Environment.NewLine}Headers:");

                foreach (var header in request.Headers)
                {
                    message.Append($"{Environment.NewLine}{JsonHelper.SerializeJson(header)}");
                }
            }

            if (request.Content != null)
            {
                message.Append($"{Environment.NewLine}Content:");
                message.Append($"{Environment.NewLine}{JsonHelper.SerializeJson(request.Content)}");
            }

            LogMessage(message.ToString());
        }

        /// <summary>
        /// Write to console http response message
        /// </summary>
        /// <param name="poco">Class</param>
        public static void LogResponse(object poco)
        {
            LogMessage($"Response:{Environment.NewLine}{JsonHelper.SerializeJson(poco)}");
        }

        /// <summary>
        /// Write to console text message
        /// </summary>
        /// <param name="messageText">String text</param>
        public static void LogMessage(string messageText)
        {
            Console.WriteLine($"{DateTimeHelper.GetCurrentDateTime()}{Environment.NewLine}{messageText}{Environment.NewLine}");
        }
    }
}