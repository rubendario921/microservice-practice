using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using multitrabajos_security.Models;
using multitrabajos_security.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace multitrabajos_security.Services
{
    public class ServiceLogin : IServiceLogin
    {
        private readonly Datacontext _datacontext;
        private readonly IConfiguration _configuration;
        public ServiceLogin(Datacontext datacontext, IConfiguration configuration)
        {
            _datacontext = datacontext;
            _configuration = configuration;
        }
        public async Task<Users> Login(LoginRequest loginRequest)
        {
            return await authUser(loginRequest.Email, loginRequest.Password);
        }

        private async Task<Users> authUser(string Email, string Password)
        {
            try
            {

                if (Email == null || Password == null)
                {
                    throw new Exception("Los datos ingresados estan vacios.");
                }
                else
                {
                    var userData = await _datacontext.Users.Where(x => x.Status.Equals("A") && x.Email.Equals(Email) && x.Password.Equals(Password)).FirstOrDefaultAsync();
                    if (userData == null)
                    {
                        throw new Exception("No existen datos registrados.");
                    }
                    else { return userData; }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }
        public object generarToken(Users user)
        {
            //Header
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                   Encoding.UTF8.GetBytes(_configuration["JWT:Clave"])
               );
            var _sigingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );

            var _Header = new JwtHeader(_sigingCredentials);
            //Claims - Publico
            var _claims = new[] {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim("nombre", user.Name),
                new Claim("apellido", user.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };
            //PayLoad
            var _Payload = new JwtPayload(
                    issuer: _configuration["JWT:Dominio"],
                    audience: _configuration["JWT:AppApi"],
                    claims: _claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(1)
                );

            //Token
            var _token = new JwtSecurityToken(_Header, _Payload);
            return new JwtSecurityTokenHandler().WriteToken(_token);
        }
    }
}
