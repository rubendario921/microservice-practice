namespace multritrabajos_withdrawal.Services
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri);
        Task<HttpResponseMessage> PostAsJsonAsync<T>(string uri, T data);
    }
}
