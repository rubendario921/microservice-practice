using Microsoft.EntityFrameworkCore;
using multritrabajos_accounts.Models;
using multritrabajos_accounts.Repository;

namespace multritrabajos_accounts.Services
{
    public class ServicesAccount : IServicesAccount
    {
        private readonly ContextDatabase _contextDatabase;
        public ServicesAccount(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }
        public async Task<IEnumerable<Account>> GetAll()
        {
            try
            {
                var result = await _contextDatabase.Account.Include(x => x.Customer).ToListAsync();
                return result;
            }
            catch (Exception)
            {
                return null!;
            }
        }
        public async Task<Account> GetbyId(int id)
        {
            try
            {
                var res = await _contextDatabase.Account.Where(x => x.IdAccount.Equals(id)).Include(x => x.Customer).AsNoTracking().FirstOrDefaultAsync();
                if (res != null)
                {
                    return res;
                }
                else { return null!; }
            }
            catch (Exception)
            {
                return null!;
            }
        }
        public async Task<bool> Deposit(Account account)
        {
            _contextDatabase.Entry(account).State = EntityState.Modified;
            return await _contextDatabase.SaveChangesAsync() > 0;
        }

        public async Task<bool> Withdrawal(Account account)
        {
            _contextDatabase.Entry(account).State = EntityState.Modified;
            return await _contextDatabase.SaveChangesAsync() > 0;
        }
    }
}