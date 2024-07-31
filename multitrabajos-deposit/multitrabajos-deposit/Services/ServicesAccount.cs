using multitrabajos_deposit.DTOs;
using multitrabajos_deposit.Models;

namespace multitrabajos_deposit.Services
{
    public class ServicesAccount : IServicesAccount
    {
        //Consumir la url del otro microservicio appsetting.json

        private readonly IConfiguration _configuration;
        private readonly IServicesTransaction _transactionService;
        private readonly IHttpClient _httpClient;


        public ServicesAccount(IConfiguration configuration, IServicesTransaction transactionService, IHttpClient httpClient)
        {
            _configuration = configuration;
            _transactionService = transactionService;
            _httpClient = httpClient;
        }

        public async Task<bool> DepositAccount(AccountRequest request)
        {
            string uri = _configuration["proxy:urlAccountDeposit"];
            var response = await _httpClient.PostAsJsonAsync(uri, request);
            response.EnsureSuccessStatusCode();
            return true;
        }

        public bool Execute(Transaction request)
        {
            bool response = false;
            AccountRequest account = new AccountRequest()
            {
                Amount = request.Amount,
                IdAccount = request.AccountId
            };
            response = DepositAccount(account).Result;
            return response;
        }
    }
}
