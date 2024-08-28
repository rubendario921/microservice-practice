using Microsoft.EntityFrameworkCore;
using multitrabajos_security.Models;
using multitrabajos_security.Repositories;

namespace multitrabajos_security.Services
{
    public class ServiceUsers : IServiceUsers
    {
        private readonly Datacontext _datacontext;
        private readonly IConfiguration _configuration;
        public ServiceUsers(Datacontext datacontext, IConfiguration configuration)
        {
            _datacontext = datacontext;
            _configuration = configuration;
        }
        public async Task<IEnumerable<DTOs.UserDTO>> GetAllUsers()
        {
            try
            {
                var result = await _datacontext.Users.Include(r => r.Rol).Select(u => new DTOs.UserDTO { Id = u.Id, Name = u.Name, LastName = u.LastName, Email = u.Email, Password = u.Password, PhoneNumber = u.PhoneNumber, Status = u.Status, DateAdd = u.DateAdd, RolID = u.RolID, RolName = u.Rol.Description }).Where(x => x.Status == "A").ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetAllUsers: {ex.Message}");
                throw;
            }
        }
        public async Task<DTOs.UserDTO> GetUserById(int id)
        {
            try
            {
                var result = await _datacontext.Users.Where(u => u.Id.Equals(id)).Include(u => u.Rol).Select(u => new DTOs.UserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    LastName = u.LastName,
                    Email = u.Email,
                    Password = u.Password,
                    PhoneNumber = u.PhoneNumber,
                    Status = u.Status,
                    DateAdd = u.DateAdd,
                    RolID = u.RolID,
                    RolName = u.Rol.Description
                }).FirstOrDefaultAsync();
                return result!;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetUserbyId: {ex.Message}");
                throw;
            }
        }
        public async Task<DTOs.UserDTO> GetUserByEmail(string email)
        {
            try
            {
                var result = await _datacontext.Users.Where(u => u.Email.Equals(email)).Include(u => u.Rol).Select(u => new DTOs.UserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    LastName = u.LastName,
                    Email = u.Email,
                    Password = u.Password,
                    PhoneNumber = u.PhoneNumber,
                    Status = u.Status,
                    DateAdd = u.DateAdd,
                    RolID = u.RolID,
                    RolName = u.Rol.Description
                }).FirstOrDefaultAsync();
                return result!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetUserbyEmail: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> SaveUser(Users user)
        {
            try
            {
                var dataUser = await GetUserByEmail(user.Email);
                if (dataUser == null)
                {                    
                    _datacontext.Users.Add(user);
                    return await _datacontext.SaveChangesAsync() > 0;
                }
                else
                {
                    throw new Exception($"Datos vacios o incompletos, intente nuevamente.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en SaveUser: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateUser(Users user)
        {
            try
            {
                var dataUser = await GetUserById(user.Id);
                if (dataUser != null)
                {
                    //Actualización de datos
                    //dataUser.Name = user.Name;
                    //dataUser.LastName = user.LastName;
                    //dataUser.Email = user.Email;
                    //dataUser.Password = user.Password;
                    //dataUser.PhoneNumber = user.PhoneNumber;
                    //dataUser.Status = "A";
                    //dataUser.DateAdd = DateTime.UtcNow;
                    //dataUser.RolID = user.RolID;

                    _datacontext.Entry(user).State = EntityState.Modified;
                    return await _datacontext.SaveChangesAsync() > 0;
                }
                else
                {
                    throw new Exception($"Error, User no registrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateUser: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var dataUser = await GetUserById(id);
                if (dataUser != null)
                {
                    dataUser.Status = "I";
                    _datacontext.Entry(dataUser).State = EntityState.Modified;
                    return await _datacontext.SaveChangesAsync() > 0;
                }
                else
                {
                    throw new Exception($"Error, User no registrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateUser: {ex.Message}");
                throw;
            }
        }
    }
}
