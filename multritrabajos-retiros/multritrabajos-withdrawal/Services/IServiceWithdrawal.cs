using multritrabajos_withdrawal.Models;

namespace multritrabajos_withdrawal.Services
{
    public interface IServiceWithdrawal
    {
        Task<WithDrawal> WithDrawal(WithDrawal withDrawal);
    }
}
