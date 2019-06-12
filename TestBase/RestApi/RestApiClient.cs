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
    /// REST API client
    /// </summary>
    public class RestApiClient : IDisposable
    {
        private static readonly HttpClient _client;
        private CancellationToken _cancellationToken;

        private bool _disposed;
        private HttpRequestMessage _request;
        private string _uri;

        static RestApiClient()
        {
            _client = new HttpClient
            {
                Timeout = new TimeSpan(0, 0, 0, 0, Timeout.Infinite)
            };
        }

        public RestApiClient(string uri)
        {
            _cancellationToken = new CancellationToken();
            _request = new HttpRequestMessage();
            _uri = uri;
        }

        /// <summary>
        /// Execute async delete http method
        /// </summary>
        /// <typeparam name="T">Poco type</typeparam>
        /// <param name="withLog">Log request and response or not?</param>
        /// <returns>Poco type of T</returns>
        public async Task<T> DeleteAsync<T>(bool withLog = true)
        {
            _request.Method = HttpMethod.Delete;
            _request.RequestUri = new Uri(_uri);

            if (withLog)
            {
                LogHelper.WriteRequest(_request);
            }

            var response = await _client.SendAsync(_request, _cancellationToken).ConfigureAwait(false);
            var poco = await ReceivePocoAsync<T>(response, withLog).ConfigureAwait(false);
            return poco;
        }

        /// <summary>
        /// Execute async get http method
        /// </summary>
        /// <typeparam name="T">Poco type</typeparam>
        /// <param name="withLog">Log request and response or not?</param>
        /// <returns>Poco type of T</returns>
        public async Task<T> GetAsync<T>(bool withLog = true)
        {
            _request.Method = HttpMethod.Get;
            _request.RequestUri = new Uri(_uri);

            if (withLog)
            {
                LogHelper.WriteRequest(_request);
            }

            var response = await _client.SendAsync(_request, _cancellationToken).ConfigureAwait(false);
            var poco = await ReceivePocoAsync<T>(response, withLog).ConfigureAwait(false);
            return poco;
        }

        /// <summary>
        /// Execute async post http method
        /// </summary>
        /// <typeparam name="T">Poco type</typeparam>
        /// <param name="withLog">Log request and response or not?</param>
        /// <returns>Poco type of T</returns>
        public async Task<T> PostAsync<T>(bool withLog = true)
        {
            _request.Method = HttpMethod.Post;
            _request.RequestUri = new Uri(_uri);

            if (withLog)
            {
                LogHelper.WriteRequest(_request);
            }

            var response = await _client.SendAsync(_request, _cancellationToken).ConfigureAwait(false);
            var poco = await ReceivePocoAsync<T>(response, withLog).ConfigureAwait(false);
            return poco;
        }

        /// <summary>
        /// Execute async put http method
        /// </summary>
        /// <typeparam name="T">Poco type</typeparam>
        /// <param name="withLog">Log request and response or not?</param>
        /// <returns>Poco type of T</returns>
        public async Task<T> PutAsync<T>(bool withLog = true)
        {
            _request.Method = HttpMethod.Put;
            _request.RequestUri = new Uri(_uri);

            if (withLog)
            {
                LogHelper.WriteRequest(_request);
            }

            var response = await _client.SendAsync(_request, _cancellationToken).ConfigureAwait(false);
            var poco = await ReceivePocoAsync<T>(response, withLog).ConfigureAwait(false);
            return poco;
        }

        private static async Task<T> ReceivePocoAsync<T>(HttpResponseMessage response, bool withLog = true)
        {
            var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var poco = JsonHelper.DeserializeJson<T>(responseText);

            if (withLog)
            {
                LogHelper.WriteResponse(poco);
            }

            return poco;
        }

        /// <summary>
        /// Add auth with login and password to request
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        public RestApiClient WithBasicAuth(string login, string password)
        {
            var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(login + ":" + password));
            WithHeaders(new Dictionary<string, string> {{"Authorization", "Basic " + base64String}});
            return this;
        }

        /// <summary>
        /// Add auth bearer token to request
        /// </summary>
        /// <param name="token">Bearer token</param>
        public RestApiClient WithBearerToken(string token)
        {
            WithHeaders(new Dictionary<string, string> {{"Authorization", "Bearer " + token}});
            return this;
        }

        /// <summary>
        /// Add http content to request
        /// </summary>
        /// <param name="content">Http content</param>
        public RestApiClient WithContent(object content)
        {
            _request.Content = JsonHelper.BuildStringContent(content);
            return this;
        }

        /// <summary>
        /// Add header to request
        /// </summary>
        /// <param name="key">Http header name</param>
        /// <param name="value">Http header value</param>
        public RestApiClient WithHeader(string key, string value)
        {
            _request.Headers.Add(key, value);
            return this;
        }

        /// <summary>
        /// Add headers to request
        /// </summary>
        /// <param name="headers">Http request headers</param>
        public RestApiClient WithHeaders(Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                _request.Headers.Add(header.Key, header.Value);
            }

            return this;
        }

        /// <summary>
        /// Build Uri from provided params
        /// </summary>
        /// <param name="parameters">Request params</param>
        public RestApiClient WithParams(object parameters = null)
        {
            _uri = StringHelper.BuildStringUri(_uri, parameters);
            return this;
        }

        /// <summary>
        /// Add Task cancel timeout to request
        /// </summary>
        /// <param name="timeout">Timeout value</param>
        public RestApiClient WithTimeout(TimeSpan timeout)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            cancellationTokenSource.CancelAfter(timeout);
            _cancellationToken = cancellationTokenSource.Token;
            return this;
        }

        protected virtual void Dispose(bool disposing)
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