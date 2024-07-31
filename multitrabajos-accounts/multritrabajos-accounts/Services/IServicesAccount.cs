namespace multritrabajos_accounts.Services
{
    public interface IServicesAccount
    {
        Task<IEnumerable<Models.Account>> GetAll();
        Task<Models.Account> GetbyId(int id);
        Task<bool> Deposit(Models.Account account);
        Task<bool> Withdrawal(Models.Account account);
    }
}
