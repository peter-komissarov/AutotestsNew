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
            var stringBuilder = new StringBuilder(
                $"{description}:"
                + $"{Environment.NewLine}"
                + $"{JsonHelper.ObjectToString(value)}");
            WriteText(stringBuilder);
        }

        /// <summary>
        /// Пишет в консоль http запрос.
        /// </summary>
        /// <param name="request">Отправляемый http запрос.</param>
        public static void WriteRequest(HttpRequestMessage request)
        {
            var stringBuilder = new StringBuilder($"{request.Method} {request.RequestUri}");

            if (request.Headers != null)
            {
                stringBuilder.Append($"{Environment.NewLine}Headers:");

                foreach (var header in request.Headers)
                {
                    stringBuilder.Append($"{Environment.NewLine}{JsonHelper.ObjectToString(header)}");
                }
            }

            if (request.Content != null)
            {
                stringBuilder.Append(
                    $"{Environment.NewLine}"
                    + $"Content:{Environment.NewLine}"
                    + $"{JsonHelper.ObjectToString(request.Content)}");
            }

            WriteText(stringBuilder);
        }

        /// <summary>
        /// Пишет в консоль http ответ.
        /// </summary>
        /// <param name="headers">Headers, которые вернул http запрос</param>
        /// <param name="poco">POCO класс.</param>
        public static void WriteResponse(HttpResponseHeaders headers, object poco)
        {
            var stringBuilder = new StringBuilder();

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (header.Key.ToLower().Contains("id"))
                    {
                        stringBuilder.Append($"{JsonHelper.ObjectToString(header)}{Environment.NewLine}");
                    }
                }

                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Insert(0, $"Headers:{Environment.NewLine}");
                }
            }

            stringBuilder.Append($"Response:{Environment.NewLine}{JsonHelper.ObjectToString(poco)}");
            WriteText(stringBuilder);
        }

        /// <summary>
        /// Пишет в консоль сообщение.
        /// </summary>
        /// <param name="stringBuilder">Текст сообщения.</param>
        private static void WriteText(StringBuilder stringBuilder)
        {
            var config = ConfigHelper.Config;

            Console.WriteLine(
                $"{DateTime.UtcNow.ToString(config["Culture&Format:DateTimeFormat"], new CultureInfo(config["Culture&Format:Language"]))}"
                + $"{Environment.NewLine}"
                + $"{stringBuilder}{Environment.NewLine}");
        }
    }
}