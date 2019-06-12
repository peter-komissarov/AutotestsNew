using System;
using System.Globalization;
using System.Net.Http;
using System.Text;

namespace TestBase.Helpers
{
    /// <summary>
    /// Console logger implementation
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// Write to console value object with description.
        /// </summary>
        /// <param name="description">Text description of object.</param>
        /// <param name="value">Value object for logging.</param>
        public static void WriteValue(string description, object value)
        {
            WriteText($"{description}:"
                + $"{Environment.NewLine}"
                + $"{value}");
        }

        /// <summary>
        /// Write to console http request message.
        /// </summary>
        /// <param name="request">HttpRequestMessage.</param>
        public static void WriteRequest(HttpRequestMessage request)
        {
            var message = new StringBuilder(
                $"Type: {request.Method}"
                + $"{Environment.NewLine}"
                + $"URI: {request.RequestUri}");

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

            WriteText(message.ToString());
        }

        /// <summary>
        /// Write to console http response message.
        /// </summary>
        /// <param name="poco">Class</param>
        public static void WriteResponse(object poco)
        {
            WriteText("Response:"
                + $"{Environment.NewLine}"
                + $"{JsonHelper.SerializeJson(poco)}");
        }

        /// <summary>
        /// Write to console text message.
        /// </summary>
        /// <param name="messageText">String text.</param>
        public static void WriteText(string messageText)
        {
            Console.WriteLine($"{DateTime.UtcNow.ToString(Configuration.DateTimeFormat, new CultureInfo(Configuration.Language))}"
                + $"{Environment.NewLine}"
                + $"{messageText}{Environment.NewLine}");
        }
    }
}