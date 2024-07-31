using Microsoft.AspNetCore.Identity.Data;

namespace multitrabajos_security.Services
{
    public interface IServiceLogin
    {
        Task<Models.Users> Login(LoginRequest loginRequest);
        object generarToken(Models.Users user);
    }
}
