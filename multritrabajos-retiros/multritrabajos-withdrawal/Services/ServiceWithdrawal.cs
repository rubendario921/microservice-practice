using multritrabajos_withdrawal.Models;
using multritrabajos_withdrawal.Repositories;

namespace multritrabajos_withdrawal.Services
{
    public class ServiceWithdrawal : IServiceWithdrawal
    {
        private readonly ContextDatabase _contextDatabase;
        public ServiceWithdrawal(ContextDatabase contextDatabase) { 
            _contextDatabase = contextDatabase;
        }
        public async Task<WithDrawal> WithDrawal(WithDrawal withDrawal)
        {
            _contextDatabase.WithDrawals.Add(withDrawal);
            await _contextDatabase.SaveChangesAsync();
            return withDrawal;
        }
    }
}
