namespace multitrabajos_deposit.Services
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri);

        Task<HttpResponseMessage> PostAsJsonAsync<T>(string uri, T item);
    }
}
