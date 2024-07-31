using multitrabajos_deposit.DTOs;
using multitrabajos_deposit.Models;

namespace multitrabajos_deposit.Services
{
    public interface IServicesAccount
    {
        Task<bool> DepositAccount(AccountRequest request);
        bool Execute(Transaction request);
    }
}
