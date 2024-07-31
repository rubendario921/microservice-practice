using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multitrabajos_security.DTOs;
using multitrabajos_security.Services;

namespace multitrabajos_security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IServiceUsers _serviceUsers;
        //Constructor
        public UsersController(IServiceUsers serviceUsers)
        {
            _serviceUsers = serviceUsers;
        }

        [HttpGet]
        //AuthorizeAttribute()
        public async Task<ActionResult> get()
        {
            var result = await _serviceUsers.getAll();
            return Ok(new
            {
                result = result,
                message = result != null ? "OK" : "Not Content"
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> getUserbyId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception($"Valor de id: {id} no es el correcto.");
                }
                else
                {
                    var result = await _serviceUsers.getUserbyId(id);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound($"No existe informacion con el id:{id}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> saveUser(UserRequest userRequest)
        {
            try
            {
                if (userRequest == null)
                {
                    throw new Exception("Los datos a ingresar estan vacios.");
                }
                else
                {
                    Models.Users userData = new Models.Users { Id = userRequest.Id, Name = userRequest.Name, LastName = userRequest.LastName, Email = userRequest.Email, Password = userRequest.Password, PhoneNumber = userRequest.PhoneNumber, RolID = userRequest.RolID };

                    var resultSave = await _serviceUsers.saveUser(userData);
                    if (resultSave)
                    {
                        return Ok(resultSave);
                    }
                    else
                    {
                        throw new Exception($"Error al registrar los datos: {userData}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }

        }
        [HttpPut]
        public async Task<ActionResult> updateUser(UserRequest userRequest)
        {
            try
            {
                if (userRequest == null)
                {
                    throw new Exception($"Los datos a ingresar estan vacios.");
                }
                else
                {
                    Models.Users userData = new Models.Users { Id = userRequest.Id, Name = userRequest.Name, LastName = userRequest.LastName, Email = userRequest.Email, Password = userRequest.Password, PhoneNumber = userRequest.PhoneNumber, RolID = userRequest.RolID };

                    var resultUpdate = await _serviceUsers.updateUser(userData);
                    if (resultUpdate)
                    {
                        return Ok(resultUpdate);
                    }
                    else
                    {
                        throw new Exception($"Error al registrar los datos: {userData}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }

        [HttpDelete]
        public async Task<ActionResult> deleteUser(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception($"Valor de Id:{id} no es el correcto.");
                }
                else
                {
                    var resultDelete = await _serviceUsers.deleteUser(id);
                    if (resultDelete)
                    {
                        return Ok(resultDelete);
                    }
                    else
                    {
                        throw new Exception($"Error en borrar los datos con el Id: {id}");
                    }
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