using multritrabajos_withdrawal.DTOs;
using multritrabajos_withdrawal.Models;
using System.Net.Http;

namespace multritrabajos_withdrawal.Services
{
    public class ServiceAccount : IServiceAccount
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClient _httpClient;
        private readonly IServiceWithdrawal _serviceWithdrawal;

        public ServiceAccount(IConfiguration configuration, IHttpClient httpClient, IServiceWithdrawal serviceWithdrawal)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _serviceWithdrawal = serviceWithdrawal;
        }
        public async Task<bool> WithdrawalAccount(AccountRequest request)
        {
            string uri = _configuration["proxy:urlAccountWithdrawal"];
            var response = await _httpClient.PostAsJsonAsync(uri, request);
            response.EnsureSuccessStatusCode();
            return true;
        }
        public bool Execute(WithDrawal request)
        {
            bool response = false;
            AccountRequest account = new AccountRequest()
            {
                   Account = request.Amount,
                   IdAccount=request.AccountId
            };
            response= WithdrawalAccount(account).Result;
            return response;
        }        
    }
}
