using multritrabajos_withdrawal.DTOs;
using multritrabajos_withdrawal.Models;

namespace multritrabajos_withdrawal.Services
{
    public interface IServiceAccount
    {
        Task<bool> WithdrawalAccount(AccountRequest request);
        bool Execute(WithDrawal request);
    }
}
