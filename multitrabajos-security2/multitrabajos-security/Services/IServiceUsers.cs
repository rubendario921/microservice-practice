namespace multitrabajos_security.Services
{
    public interface IServiceUsers
    {
        Task<IEnumerable<Models.Users>> getAll();
        Task<Models.Users> getUserbyId(int id);
        Task<Models.Users> getUserbyEmail(string email);
        Task<bool> saveUser(Models.Users user);
        Task<bool> updateUser(Models.Users user);
        Task<bool> deleteUser(int id);
    }
}