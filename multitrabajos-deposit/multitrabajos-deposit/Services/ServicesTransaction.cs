using multitrabajos_deposit.Models;
using multitrabajos_deposit.Repositories;

namespace multitrabajos_deposit.Services
{
    public class ServicesTransaction: IServicesTransaction
    {
        private readonly ContextDatabase _contextDatabase;

        public ServicesTransaction(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public async Task<Transaction> Deposit(Transaction transaction)
        {
            _contextDatabase.Transaction.Add(transaction);
            await _contextDatabase.SaveChangesAsync();
            return transaction;
        }
    }
}
