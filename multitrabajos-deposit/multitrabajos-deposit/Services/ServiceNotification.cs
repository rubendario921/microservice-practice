using multitrabajos_deposit.DTOs;
using multitrabajos_deposit.Models;

namespace multitrabajos_deposit.Services
{
    public class ServiceNotification : IServiceNotification
    {
        private readonly IConfiguration _configuration;
        private readonly IServicesTransaction _transactionService;
        private readonly IHttpClient _httpClient;

        public ServiceNotification(IConfiguration configuration, IServicesTransaction transactionService, IHttpClient httpClient)
        {
            _configuration = configuration;
            _transactionService = transactionService;
            _httpClient = httpClient;
        }
        public async Task<bool> NotificationAccount(NotificationRequest request)
        {
            string uri = _configuration["proxy:urlNotification"];
            var response = await _httpClient.PostAsJsonAsync(uri, request);
            response.EnsureSuccessStatusCode();
            return true;
        }

        public bool Execute(Transaction request)
        {
            bool response = false;
            NotificationRequest notification = new NotificationRequest()
            {
                Ammount = request.Amount,
                Type = request.Type,
                IdAccount = request.AccountId,
                IdCustomer = request.Id,
            };
            response = NotificationAccount(notification).Result;
            return response;
        }
    }
}
