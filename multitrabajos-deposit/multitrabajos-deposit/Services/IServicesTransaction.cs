using multitrabajos_deposit.Models;

namespace multitrabajos_deposit.Services
{
    public interface IServicesTransaction
    {
        Task<Transaction> Deposit(Transaction transaction);
    }
}
