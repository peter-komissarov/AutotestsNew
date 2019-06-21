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
                + $"{JsonHelper.Serialize(value)}");

            WriteText(stringBuilder.ToString());
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
                stringBuilder.Append($"{Environment.NewLine}With headers:");

                foreach (var header in request.Headers)
                {
                    stringBuilder.Append($"{Environment.NewLine}{JsonHelper.Serialize(header)}");
                }
            }

            if (request.Content != null)
            {
                stringBuilder.Append(
                    $"{Environment.NewLine}"
                    + $"With content:{Environment.NewLine}"
                    + $"{JsonHelper.Serialize(request.Content)}");
            }

            WriteText(stringBuilder.ToString());
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
                        stringBuilder.Append($"{JsonHelper.Serialize(header)}{Environment.NewLine}");
                    }
                }

                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Insert(0, $"Response headers:{Environment.NewLine}");
                }
            }

            stringBuilder.Append($"Response content:{Environment.NewLine}{JsonHelper.Serialize(poco)}");
            WriteText(stringBuilder.ToString());
        }

        /// <summary>
        /// Пишет в консоль сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        public static void WriteText(string message)
        {
            var config = ConfigHelper.Configuration;

            Console.WriteLine(
                $"{DateTime.UtcNow.ToString(config["Format:DateTime"], new CultureInfo(config["Format:Language"]))}"
                + $"{Environment.NewLine}"
                + $"{message}{Environment.NewLine}"
                + $"{Environment.NewLine}");
        }
    }
}