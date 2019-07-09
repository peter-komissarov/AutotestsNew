using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestBase.Helpers;

namespace TestBase.Clients
{
    /// <summary>
    /// Реализация REST API клиента.
    /// </summary>
    public class Client : IDisposable
    {
        private static readonly HttpClient _client;
        private CancellationToken _cancellationToken;
        private bool _disposed;
        private HttpRequestMessage _request;
        private string _uri;
        private bool _withLog;

        static Client()
        {
            _client = new HttpClient
            {
                Timeout = new TimeSpan(0, 0, 0, 0, Timeout.Infinite)
            };
        }

        public Client()
        {
            _cancellationToken = new CancellationToken();
            _request = new HttpRequestMessage();
            _withLog = true;
        }

        /// <summary>
        /// Асинхронно выполняет delete http запрос.
        /// </summary>
        /// <typeparam name="T">POCO класс.</typeparam>
        /// <returns>Ответ от API в формате POCO класс.</returns>
        public async ValueTask<T> DeleteAsync<T>()
        {
            _request.Method = HttpMethod.Delete;
            _request.RequestUri = new Uri(_uri);

            if (_withLog)
            {
                LogProvider.WriteRequest(_request);
            }

            using var response = await _client.SendAsync(_request, _cancellationToken).ConfigureAwait(false);
            var poco = await ReceivePocoAsync<T>(response).ConfigureAwait(false);

            return poco;
        }

        /// <summary>
        /// Асинхронно выполняет get http запрос.
        /// </summary>
        /// <typeparam name="T">POCO класс.</typeparam>
        /// <returns>Ответ от API в формате POCO класс.</returns>
        public async ValueTask<T> GetAsync<T>()
        {
            _request.Method = HttpMethod.Get;
            _request.RequestUri = new Uri(_uri);

            if (_withLog)
            {
                LogProvider.WriteRequest(_request);
            }

            using var response = await _client.SendAsync(_request, _cancellationToken).ConfigureAwait(false);
            var poco = await ReceivePocoAsync<T>(response).ConfigureAwait(false);

            return poco;
        }

        /// <summary>
        /// Асинхронно выполняет post http запрос.
        /// </summary>
        /// <typeparam name="T">POCO класс.</typeparam>
        /// <returns>Ответ от API в формате POCO класс.</returns>
        public async ValueTask<T> PostJsonAsync<T>()
        {
            _request.Method = HttpMethod.Post;
            _request.RequestUri = new Uri(_uri);

            if (_withLog)
            {
                LogProvider.WriteRequest(_request);
            }

            using var response = await _client.SendAsync(_request, _cancellationToken).ConfigureAwait(false);
            var poco = await ReceivePocoAsync<T>(response).ConfigureAwait(false);

            return poco;
        }

        /// <summary>
        /// Асинхронно выполняет put http запрос.
        /// </summary>
        /// <typeparam name="T">POCO класс.</typeparam>
        /// <returns>Ответ от API в формате POCO класс.</returns>
        public async ValueTask<T> PutJsonAsync<T>()
        {
            _request.Method = HttpMethod.Put;
            _request.RequestUri = new Uri(_uri);

            if (_withLog)
            {
                LogProvider.WriteRequest(_request);
            }

            using var response = await _client.SendAsync(_request, _cancellationToken).ConfigureAwait(false);
            var poco = await ReceivePocoAsync<T>(response).ConfigureAwait(false);

            return poco;
        }

        public async ValueTask<T> ReceivePocoAsync<T>(HttpResponseMessage response)
        {
            var content = response.Content;
            var responseText = await content.ReadAsStringAsync().ConfigureAwait(false);
            var poco = JsonProvider.Deserialize<T>(responseText);

            if (_withLog)
            {
                LogProvider.WriteResponse(response.Headers, poco);
            }

            return poco;
        }

        public Client SetLanguage(ref HttpRequestMessage request, string language = null)
        {
            var acceptLanguage = language ?? AppSettingsProvider.Configuration["Format:Language"];
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(acceptLanguage));

            return this;
        }

        /// <summary>
        /// Добавляет к http запросу хедер для авторизации пользователя по логину и паролю.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        public Client WithBasicAuth(string login, string password)
        {
            var authHeaderString = $"{login}:{password}";
            var authHeaderBytes = Encoding.UTF8.GetBytes(authHeaderString);
            var base64String = Convert.ToBase64String(authHeaderBytes);
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
            _request.Content = ObjectProvider.ToStringContent(content);

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
        /// Включает, либо выключает запись лога запроса и ответа в консоль
        /// </summary>
        /// <param name="withLog">Требуется ли выводить в консоль http запрос и ответ?</param>
        /// <returns></returns>
        public Client WithLog(bool withLog)
        {
            _withLog = withLog;

            return this;
        }

        /// <summary>
        /// Добавляет параметры к Uri.
        /// </summary>
        /// <param name="parameters">Параметры.</param>
        public Client WithParams(object parameters = null)
        {
            _uri = StringProvider.GetAbsoluteUri(_uri, parameters);

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

        /// <summary>
        /// Добавляет Uri к http запросу.
        /// </summary>
        /// <param name="uri">Унифицированный идентификатор ресурса.</param>
        public Client WithUri(string uri)
        {
            _uri = uri;

            return this;
        }

        private void RunDispose()
        {
            if (_disposed)
            {
                return;
            }

            _request?.Dispose();
            _request = null;
            _cancellationToken = CancellationToken.None;
            _uri = string.Empty;

            _disposed = true;
        }

        public void Dispose()
        {
            RunDispose();
            GC.SuppressFinalize(this);
        }
    }
}