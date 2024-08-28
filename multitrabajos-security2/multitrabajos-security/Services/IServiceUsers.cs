namespace multitrabajos_security.Services
{
    public interface IServiceUsers
    {
        Task<IEnumerable<DTOs.UserDTO>> GetAllUsers();
        Task<DTOs.UserDTO> GetUserById(int id);
        Task<DTOs.UserDTO> GetUserByEmail(string email);
        Task<bool> SaveUser(Models.Users user);
        Task<bool> UpdateUser(Models.Users user);
        Task<bool> DeleteUser(int id);
        
    }
}