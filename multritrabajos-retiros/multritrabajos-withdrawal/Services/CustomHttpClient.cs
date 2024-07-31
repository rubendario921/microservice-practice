
using Newtonsoft.Json;

namespace multritrabajos_withdrawal.Services
{
    public class CustomHttpClient : IHttpClient
    {
        private HttpClient _httpClient;
        public CustomHttpClient()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> GetStringAsync(string uri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await _httpClient.SendAsync(requestMessage);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string uri, T data)
        {
            return await DoPostPutAsync(HttpMethod.Post, uri, data);
        }

        private async Task<HttpResponseMessage> DoPostPutAsync<T>(HttpMethod method, string uri, T data)
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }
            var requestMessage = new HttpRequestMessage(method, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(requestMessage);
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError) {
                throw new HttpRequestException();
            }
            return response;

        }
    }
}
