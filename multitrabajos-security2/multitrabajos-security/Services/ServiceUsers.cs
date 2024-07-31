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
        public async Task<IEnumerable<Users>> getAll()
        {
            try
            {
                var result = await _datacontext.Users.Where(x => x.Status.Equals("A")).ToListAsync();
                if(!result.Any()) {
                    throw new Exception("No existe informacion registrada.");
                }
                else { 
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task<Users> getUserbyId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception($"El valor del Id:{id} no es correcto.");
                }
                else
                {
                    var result = await _datacontext.Users.Where(x => x.Status.Equals("A") && x.Id.Equals(id)).FirstOrDefaultAsync();
                    if (result == null)
                    {
                        throw new Exception($"No existe informacion con el id:{id} ingresado.");
                    }
                    else { return result; }                    
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task<Users> getUserbyEmail(string email)
        {
            try
            {
                if (email ==null)
                {
                    throw new Exception($"El email :{email} tiene un campo vacio.");
                }
                else
                {
                    var result = await _datacontext.Users.Where(x => x.Status.Equals("A") && x.Email.Equals(email)).FirstOrDefaultAsync();
                    if (result == null)
                    {
                        throw new Exception($"No existe informacion con el correo: {email} ingresado.");
                    }
                    else { return result; }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }        
        public async Task<bool> saveUser(Users user)
        {
            try
            {
                var userExist = await getUserbyEmail(user.Email);
                if (userExist == null)
                {
                    throw new Exception($"El {userExist} no esta registrado.");
                }
                else
                {
                    user.DateAdd = DateTime.UtcNow;
                    user.Status = "A";
                    _datacontext.Add(user);
                    return await _datacontext.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }

        public async Task<bool> updateUser(Users user)
        {
            try
            {
                var userExist = await getUserbyId(user.Id);
                if (userExist == null)
                {
                    throw new Exception($"El {userExist} no esta registrado.");
                }
                else
                {
                    _datacontext.Entry(user).State = EntityState.Modified;
                    return await _datacontext.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }
        public async Task<bool> deleteUser(int id)
        {
            try
            {
                var userExist = await getUserbyId(id);
                if (userExist == null)
                {
                    throw new Exception($"El {userExist} no esta registrado.");
                }
                else
                {
                    userExist.Status = "I";
                    _datacontext.Entry(userExist).State = EntityState.Modified;
                    return await _datacontext.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }
    }
}
