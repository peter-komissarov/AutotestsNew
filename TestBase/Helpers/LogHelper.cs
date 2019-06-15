using System;
using System.Globalization;
using System.Net.Http;
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
            var message = new StringBuilder(
                $"Type: {request.Method}"
                + $"{Environment.NewLine}"
                + $"URI: {request.RequestUri}");

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
                message.Append($"{Environment.NewLine}Content:");
                message.Append($"{Environment.NewLine}{JsonHelper.ObjectToString(request.Content)}");
            }

            WriteText(message.ToString());
        }

        /// <summary>
        /// Пишет в консоль http ответ.
        /// </summary>
        /// <param name="poco">POCO класс.</param>
        public static void WriteResponse(object poco)
        {
            WriteText("Response:"
                + $"{Environment.NewLine}"
                + $"{JsonHelper.ObjectToString(poco)}");
        }

        /// <summary>
        /// Пишет в консоль сообщение.
        /// </summary>
        /// <param name="messageText">Текст сообщения.</param>
        public static void WriteText(string messageText)
        {
            Console.WriteLine($"{DateTime.UtcNow.ToString(Configuration.DateTimeFormat, new CultureInfo(Configuration.Language))}"
                + $"{Environment.NewLine}"
                + $"{messageText}{Environment.NewLine}");
        }
    }
}