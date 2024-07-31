using multritrabajos_withdrawal.DTOs;
using multritrabajos_withdrawal.Models;

namespace multritrabajos_withdrawal.Services
{
    public class ServiceNotification : IServiceNotification
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceWithdrawal _serviceWithdrawal;
        private readonly IHttpClient _httpClient;

        public ServiceNotification(IConfiguration configuration, IServiceWithdrawal serviceWithdrawal, IHttpClient httpClient)
        {
            _configuration = configuration;
            _serviceWithdrawal = serviceWithdrawal;
            _httpClient = httpClient;
        }

        public async Task<bool> NotificationAccount(NotificationRequest request)
        {
            string uri = _configuration["proxy:urlNotification"];
            var response = await _httpClient.PostAsJsonAsync(uri, request);
            response.EnsureSuccessStatusCode();
            return true;
        }
        public bool Execute(WithDrawal request)
        {
            bool response = false;
            NotificationRequest notificationRequest = new NotificationRequest() { Ammount = request.Amount, Type = request.Type, IdAccount = request.AccountId, IdCustomer = request.Id, };
            response = NotificationAccount(notificationRequest).Result;
            return response;
        }
    }
}
