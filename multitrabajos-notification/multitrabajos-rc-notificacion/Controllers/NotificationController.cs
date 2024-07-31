using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multitrabajos_rc_notificacion.DTOs;
using multitrabajos_rc_notificacion.Services;

namespace multitrabajos_rc_notificacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IServiceNotification _serviceNotification;
        public NotificationController(IServiceNotification serviceNotification)
        {
            _serviceNotification = serviceNotification;
        }

        //Metodos
        [HttpPost]
        [Route("Notificacion")]
        public async Task<ActionResult> Notification(NotificationRequest request)
        {
            if (request == null)
            {
                return NotFound("Los datos ingresados son nulos.");
                ;
            }
            else
            {
                Models.Notification notification = new Models.Notification()
                {
                    Ammount = request.Ammount,
                    Type = request.Type,
                    IdAccount = request.IdAccount,
                    IdCustomer = request.IdCustomer,
                };

                notification = await _serviceNotification.Notification(notification);
                if (notification != null)
                {
                    return Ok("Informacion registrado de la manera correcta.");
                }
                else
                {
                    return BadRequest("Notificacion no registrada.");
                }

            }
        }
    }
}
