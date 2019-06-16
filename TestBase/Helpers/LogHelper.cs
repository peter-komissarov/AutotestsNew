using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TestBase.Helpers
{
    /// <summary>
    /// Предоставляет вспомогательные методы, для вывода сообщений в консоль.
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// Пишет в консоль описание объекта и его значение.
        /// </summary>
        /// <param name="description">Описание объекта.</param>
        /// <param name="value">Значение объекта.</param>
        public static void WriteValue(string description, object value)
        {
            WriteText($"{description}:"
                + $"{Environment.NewLine}"
                + $"{JsonHelper.ObjectToString(value)}");
        }

        /// <summary>
        /// Пишет в консоль http запрос.
        /// </summary>
        /// <param name="request">Отправляемый http запрос.</param>
        public static void WriteRequest(HttpRequestMessage request)
        {
            var message = new StringBuilder($"{request.Method} {request.RequestUri}");

            if (request.Headers != null)
            {
                message.Append($"{Environment.NewLine}Headers:");

                foreach (var header in request.Headers)
                {
                    message.Append($"{Environment.NewLine}{JsonHelper.ObjectToString(header)}");
                }
            }

            if (request.Content != null)
            {
                message.Append($"{Environment.NewLine}"
                    + $"Content:{Environment.NewLine}"
                    + $"{JsonHelper.ObjectToString(request.Content)}");
            }

            WriteText(message.ToString());
        }

        /// <summary>
        /// Пишет в консоль http ответ.
        /// </summary>
        /// <param name="headers">Headers, которые вернул http запрос</param>
        /// <param name="poco">POCO класс.</param>
        public static void WriteResponse(HttpResponseHeaders headers, object poco)
        {
            var message = new StringBuilder();

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (header.Key.ToLower().Contains("id"))
                    {
                        message.Append($"{JsonHelper.ObjectToString(header)}{Environment.NewLine}");
                    }
                }

                if (message.Length > 0)
                {
                    message.Insert(0, $"Headers:{Environment.NewLine}");
                }
            }

            message.Append($"Response:{Environment.NewLine}{JsonHelper.ObjectToString(poco)}");
            WriteText(message.ToString());
        }

        /// <summary>
        /// Пишет в консоль сообщение.
        /// </summary>
        /// <param name="messageText">Текст сообщения.</param>
        private static void WriteText(string messageText)
        {
            Console.WriteLine($"{DateTime.UtcNow.ToString(ConfigurationHelper.TestConfig["Culture&Format:DateTimeFormat"], new CultureInfo(ConfigurationHelper.TestConfig["Culture&Format:Language"]))}"
                + $"{Environment.NewLine}"
                + $"{messageText}{Environment.NewLine}");
        }
    }
}