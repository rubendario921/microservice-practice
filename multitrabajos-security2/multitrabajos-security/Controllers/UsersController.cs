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
        [Route("GetAllUsers")]
        //AuthorizeAttribute()
        public async Task<ActionResult<IEnumerable<DTOs.UserDTO>>> GetAllUsers()
        {
            var result = await _serviceUsers.GetAllUsers();
            if (result == null)
            {
                return NotFound($"No existe informacion registrada.");
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<ActionResult<DTOs.UserDTO>> GetUserById(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Error, Id {id} vacio o incorrecto.");
            }
            var result = await _serviceUsers.GetUserById(id);
            if (result == null)
            {
                return BadRequest($"No existe informacion por ID: {id} registrado.");
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("SaveUser")]
        public async Task<ActionResult<bool>> SaveUser(DTOs.UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest($"Campos vacios o incorrectos, intente nuevamente.");
            }
            //Transformar de DTO a Model
            Models.Users dataUser = new()
            {
                Name = userDTO.Name,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Password = userDTO.Password,
                PhoneNumber = userDTO.PhoneNumber,
                Status = "A",
                DateAdd = DateTime.UtcNow,
                RolID = userDTO.RolID,
            };

            var result = await _serviceUsers.SaveUser(dataUser);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest($"Error, usuario registrado");
            }
        }
        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task<ActionResult<bool>> UpdateUser(int id, DTOs.UserDTO userDTO)
        {
            if (id <= 0) { return BadRequest($"Id: {id} incorrecto o vacio."); }

            var consult = await _serviceUsers.GetUserById(id);
            if (consult == null)
            {
                return BadRequest($"Error, User por Id: {id} no registrado");
            }

            if (userDTO == null)
            {
                return BadRequest($"Campos vacios o incorrectos, intente nuevamente.");
            }

            //Transformar DTO a Model
            Models.Users dataUser = new()
            {
                Id = consult.Id,
                Name = userDTO.Name,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Password = userDTO.Password,
                PhoneNumber = userDTO.PhoneNumber,
                Status = "A",
                DateAdd = DateTime.UtcNow,
                RolID = userDTO.RolID,
            };

            var result = await _serviceUsers.UpdateUser(dataUser);
            if (result) { return Ok(result); }
            else
            {
                return BadRequest($"Error, User no actualizado, campos incorrectos o vacios");
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            if (id <= 0) { return BadRequest($"Id: {id} incorrecto o vacio."); }
            var result = await _serviceUsers.DeleteUser(id);
            if (result) { return Ok(result); } else { return BadRequest($"Error, User no eliminado, intente nuevamnete"); }


        }
    }
}