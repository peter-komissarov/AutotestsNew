using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestBase.Helpers;

namespace TestBase.HttpClients
{
    internal static class BaseHttpClient
    {
        internal static (HttpClient httpClient, HttpRequestMessage request) CreateRequest(this HttpClient client)
        {
            return (httpClient: client, request: new HttpRequestMessage());
        }

        internal static (HttpClient httpClient, HttpRequestMessage request) WithBearerToken(
            this (HttpClient httpClient, HttpRequestMessage request) clientWithRequest,
            string token)
        {
            return clientWithRequest.WithHeaders(new[] { new KeyValuePair<string, string>("Authorization", "Bearer " + token) });
        }

        internal static (HttpClient httpClient, HttpRequestMessage request) WithHeaders(
            this (HttpClient httpClient, HttpRequestMessage request) clientWithRequest,
            IEnumerable<KeyValuePair<string, string>> requestHeaders)
        {
            requestHeaders?.AsParallel().AsUnordered().Where(i => i.Key != null).ForAll(i => clientWithRequest.request?.Headers?.Add(i.Key, i.Value));
            return clientWithRequest;
        }

        internal static (HttpClient httpClient, HttpRequestMessage request) WithLoginPasswordAuth(
            this (HttpClient httpClient, HttpRequestMessage request) clientWithRequest,
            string login,
            string password)
        {
            var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(login + ":" + password));
            return clientWithRequest.WithHeaders(new[] { new KeyValuePair<string, string>("Authorization", "Basic " + base64String) });
        }

        internal static (HttpClient httpClient, HttpRequestMessage request) WithRequestUri(
            this (HttpClient httpClient, HttpRequestMessage request) clientWithRequest,
            string requestUri)
        {
            clientWithRequest.request.RequestUri = new Uri(requestUri, UriKind.Relative);
            return clientWithRequest;
        }

        internal static (HttpClient httpClient, HttpRequestMessage request) WithTimeout(
            this (HttpClient httpClient, HttpRequestMessage request) clientWithRequest,
            TimeSpan timeout)
        {
            clientWithRequest.httpClient.Timeout = timeout;
            return clientWithRequest;
        }

        internal static async Task<HttpResponseMessage> DeleteAsync(this (HttpClient httpClient, HttpRequestMessage request) clientWithRequest, IEnumerable<KeyValuePair<string, string>> paramsCollection = null, bool withLog = true)
        {
            clientWithRequest.request.Method = HttpMethod.Delete;

            if (withLog)
            {
                Logger.Request(clientWithRequest, null);
            }

            return await clientWithRequest.httpClient.SendAsync(clientWithRequest.request).ConfigureAwait(false);
        }

        internal static async Task<HttpResponseMessage> GetAsync(this (HttpClient httpClient, HttpRequestMessage request) clientWithRequest, IEnumerable<KeyValuePair<string, string>> paramsCollection = null, bool withLog = true)
        {
            clientWithRequest.request.Method = HttpMethod.Get;

            if (withLog)
            {
                Logger.Request(clientWithRequest, null);
            }

            return await clientWithRequest.httpClient.SendAsync(clientWithRequest.request).ConfigureAwait(false);
        }

        internal static async Task<HttpResponseMessage> PostAsync(this (HttpClient httpClient, HttpRequestMessage request) clientWithRequest, object content, bool withLog = true)
        {
            clientWithRequest.request.Method = HttpMethod.Post;

            if (withLog)
            {
                Logger.Request(clientWithRequest, content);
            }

            clientWithRequest.request.Content = content.BuildStringContent();
            return await clientWithRequest.httpClient.SendAsync(clientWithRequest.request).ConfigureAwait(false);
        }

        internal static async Task<HttpResponseMessage> PutAsync(this (HttpClient httpClient, HttpRequestMessage request) clientWithRequest, object content, bool withLog = true)
        {
            clientWithRequest.request.Method = HttpMethod.Put;

            if (withLog)
            {
                Logger.Request(clientWithRequest, content);
            }

            clientWithRequest.request.Content = content.BuildStringContent();
            return await clientWithRequest.httpClient.SendAsync(clientWithRequest.request).ConfigureAwait(false);
        }

        internal static async Task<T> ReceiveJsonAsync<T>(this Task<HttpResponseMessage> responseTask, bool withLog = true)
        {
            using (var response = await responseTask.ConfigureAwait(false))
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var poco = JsonConvert.DeserializeObject<T>(responseText);

                if (withLog)
                {
                    Logger.Response(poco);
                }

                return poco;
            }
        }

        private static StringContent BuildStringContent(this object content)
        {
            var json = JsonConvert.SerializeObject(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}