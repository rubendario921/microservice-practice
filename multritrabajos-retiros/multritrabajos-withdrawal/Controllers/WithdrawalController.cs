using Cordillera.Distribuidas.Event.Bus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multritrabajos_withdrawal.DTOs;
using multritrabajos_withdrawal.Messages.Commands;
using multritrabajos_withdrawal.Models;
using multritrabajos_withdrawal.Services;

namespace multritrabajos_withdrawal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawalController : ControllerBase
    {
        private readonly IServiceWithdrawal _serviceWithdrawal;
        private readonly IServiceAccount _serviceAccount;
        private readonly IServiceNotification _serviceNotification;
        private readonly IEventBus _bus;
        public WithdrawalController(IEventBus eventBus,IServiceWithdrawal serviceWithdrawal, IServiceAccount serviceAccount, IServiceNotification serviceNotification)
        {
            _serviceWithdrawal = serviceWithdrawal;
            _serviceAccount = serviceAccount;
            _serviceNotification = serviceNotification;
            _bus = eventBus;
        }
        [HttpPost("Retiros")]
        public async Task<ActionResult> WithDrawal(WithdrawalRequest request)
        {
            if (request == null)
            {
                return BadRequest("Los datos ingresados estan vacios.");
            }
            WithDrawal withdrawal = new WithDrawal()
            {
                AccountId = request.AccountId,
                Amount = request.Ammount,
                CreationDate = DateTime.Now.ToShortDateString(),
                Type = "Retiro"
            };
            //Registrar en la base de datos el retiro
            withdrawal = await _serviceWithdrawal.WithDrawal(withdrawal);            

            //Consulta de base de datos de Cuenta
            bool isProcessed = _serviceAccount.Execute(withdrawal);
            if (isProcessed) {
                //Enviar la notificacion
                bool complete = _serviceNotification.Execute(withdrawal);
                if (complete) {
                    //Enviar el historial de RabbitQM
                    var transactionCreateCommand = new TransactionCreateCommand(idTransaction: withdrawal.Id, amount: withdrawal.Amount, type: withdrawal.Type, creationDate: withdrawal.CreationDate, accountId: withdrawal.AccountId);
                    await _bus.SendCommand(transactionCreateCommand);
                }
            }
            
            //El proceso es correcto
            return Ok(withdrawal);
        }

    }
}
