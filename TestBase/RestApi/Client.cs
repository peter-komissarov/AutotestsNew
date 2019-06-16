using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestBase.Helpers;

namespace TestBase.RestApi
{
    /// <summary>
    /// Реализация REST API клиента.
    /// </summary>
    public sealed class Client : IDisposable
    {
        private static readonly HttpClient _client;
        private CancellationToken _cancellationToken;

        private bool _disposed;
        private HttpRequestMessage _request;
        private string _uri;

        static Client()
        {
            _client = new HttpClient
            {
                Timeout = new TimeSpan(0, 0, 0, 0, Timeout.Infinite)
            };
        }

        public Client(string uri)
        {
            _cancellationToken = new CancellationToken();
            _request = new HttpRequestMessage();
            _uri = uri;
        }

        /// <summary>
        /// Асинхронно выполняет delete http запрос.
        /// </summary>
        /// <typeparam name="T">POCO класс.</typeparam>
        /// <param name="withLog">Требуется ли выводить в консоль http запрос и ответ.</param>
        /// <returns>Ответ от API в формате POCO класс.</returns>
        public async Task<T> DeleteAsync<T>(bool withLog = true)
        {
            _request.Method = HttpMethod.Delete;
            _request.RequestUri = new Uri(_uri);

            if (withLog)
            {
                LogHelper.WriteRequest(_request);
            }

            var response = await _client
                .SendAsync(_request, _cancellationToken)
                .ConfigureAwait(false);

            var poco = await ReceivePocoAsync<T>(response, withLog)
                .ConfigureAwait(false);

            return poco;
        }

        /// <summary>
        /// Асинхронно выполняет get http запрос.
        /// </summary>
        /// <typeparam name="T">POCO класс.</typeparam>
        /// <param name="withLog">Требуется ли выводить в консоль http запрос и ответ.</param>
        /// <returns>Ответ от API в формате POCO класс.</returns>
        public async Task<T> GetAsync<T>(bool withLog = true)
        {
            _request.Method = HttpMethod.Get;
            _request.RequestUri = new Uri(_uri);

            if (withLog)
            {
                LogHelper.WriteRequest(_request);
            }

            var response = await _client
                .SendAsync(_request, _cancellationToken)
                .ConfigureAwait(false);

            var poco = await ReceivePocoAsync<T>(response, withLog)
                .ConfigureAwait(false);

            return poco;
        }

        /// <summary>
        /// Асинхронно выполняет post http запрос.
        /// </summary>
        /// <typeparam name="T">POCO класс.</typeparam>
        /// <param name="withLog">Требуется ли выводить в консоль http запрос и ответ.</param>
        /// <returns>Ответ от API в формате POCO класс.</returns>
        public async Task<T> PostJsonAsync<T>(bool withLog = true)
        {
            _request.Method = HttpMethod.Post;
            _request.RequestUri = new Uri(_uri);

            if (withLog)
            {
                LogHelper.WriteRequest(_request);
            }

            var response = await _client
                .SendAsync(_request, _cancellationToken)
                .ConfigureAwait(false);

            var poco = await ReceivePocoAsync<T>(response, withLog)
                .ConfigureAwait(false);

            return poco;
        }

        /// <summary>
        /// Асинхронно выполняет put http запрос.
        /// </summary>
        /// <typeparam name="T">POCO класс.</typeparam>
        /// <param name="withLog">Требуется ли выводить в консоль http запрос и ответ.</param>
        /// <returns>Ответ от API в формате POCO класс.</returns>
        public async Task<T> PutJsonAsync<T>(bool withLog = true)
        {
            _request.Method = HttpMethod.Put;
            _request.RequestUri = new Uri(_uri);

            if (withLog)
            {
                LogHelper.WriteRequest(_request);
            }

            var response = await _client
                .SendAsync(_request, _cancellationToken)
                .ConfigureAwait(false);

            var poco = await ReceivePocoAsync<T>(response, withLog)
                .ConfigureAwait(false);

            return poco;
        }

        private static async Task<T> ReceivePocoAsync<T>(HttpResponseMessage response, bool withLog = true)
        {
            var responseText = await response
                .Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            var poco = JsonHelper.StringToPoco<T>(responseText);

            if (withLog)
            {
                LogHelper.WriteResponse(response.Headers, poco);
            }

            return poco;
        }

        /// <summary>
        /// Добавляет к http запросу хедер для авторизации пользователя по логину и паролю.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        public Client WithBasicAuth(string login, string password)
        {
            var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(login + ":" + password));
            WithHeaders(new Dictionary<string, string> {{"Authorization", "Basic " + base64String}});

            return this;
        }

        /// <summary>
        /// Добавляет к http запросу хедер для авторизации пользователя по bearer токену.
        /// </summary>
        /// <param name="token">Bearer токен.</param>
        public Client WithBearerToken(string token)
        {
            WithHeaders(new Dictionary<string, string> {{"Authorization", "Bearer " + token}});

            return this;
        }

        /// <summary>
        /// Добавляет контент к post и put http запросам.
        /// </summary>
        /// <param name="content">Контент.</param>
        public Client WithContent(object content)
        {
            _request.Content = JsonHelper.ObjectToStringContent(content);

            return this;
        }

        /// <summary>
        /// Добавляет хедер к http запросу.
        /// </summary>
        /// <param name="key">Имя хедера.</param>
        /// <param name="value">Значение хедера.</param>
        public Client WithHeader(string key, string value)
        {
            _request.Headers.Add(key, value);

            return this;
        }

        /// <summary>
        /// Добавляет коллекцию хедеров к http запросу.
        /// </summary>
        /// <param name="headers">Словарь из сущностей типа 'имя хедера, значение хедера'.</param>
        public Client WithHeaders(Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                _request.Headers.Add(header.Key, header.Value);
            }

            return this;
        }

        /// <summary>
        /// Добавляет параметры к Uri.
        /// </summary>
        /// <param name="parameters">Параметры.</param>
        public Client WithParams(object parameters = null)
        {
            _uri = StringHelper.GetAbsoluteUri(_uri, parameters);
            return this;
        }

        /// <summary>
        /// Добавляет время ожидания завершения http запроса.
        /// </summary>
        /// <param name="timeout">Время ожидания завершения асинхронной задачи.</param>
        public Client WithTimeout(TimeSpan timeout)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(timeout);
            _cancellationToken = cancellationTokenSource.Token;

            return this;
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _request?.Dispose();
                _request = null;

                _cancellationToken = CancellationToken.None;
                _uri = string.Empty;
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}