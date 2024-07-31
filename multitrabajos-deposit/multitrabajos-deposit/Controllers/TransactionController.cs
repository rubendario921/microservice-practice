using Cordillera.Distribuidas.Event.Bus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multitrabajos_deposit.DTOs;
using multitrabajos_deposit.Messages.Commands;
using multitrabajos_deposit.Services;

namespace multitrabajos_deposit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IServicesTransaction _transactionService;
        private readonly IServicesAccount _accountService;
        private readonly IServiceNotification _servicelistNotification;
        private readonly IEventBus _bus;
        public TransactionController(IEventBus bus, IServicesTransaction transactionService, IServicesAccount accountService, IServiceNotification servicelistNotification)
        {
            _transactionService = transactionService;
            _accountService = accountService;
            _servicelistNotification = servicelistNotification;
            _bus = bus;
        }
        //Metodos
        [HttpPost("Deposit")]
        public async Task<ActionResult> Deposit(TransactionRequest request)
        {
            if (request == null)
            {
                return BadRequest("This request is null");
            }
            Models.Transaction transaction = new Models.Transaction()
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                CreationDate = DateTime.Now.ToShortDateString(),
                Type = "Deposit"
            };
            //Registrar en la base de datos el deposito
            transaction = await _transactionService.Deposit(transaction);

            //Consulta la base de datos de Cuenta
            bool isProccess = _accountService.Execute(transaction);
            if (isProccess)
            {
                //Registra en la base de datos de notificaciones
                bool isNotification = _servicelistNotification.Execute(transaction);
                if (isNotification)
                {
                    //Enviar el Historial de RabbittQM
                    var transactionCreateCommand = new TransactionCreateCommand(idTransaction: transaction.Id, amount: transaction.Amount, type: transaction.Type, creationDate: transaction.CreationDate, accountId: transaction.AccountId);
                    await _bus.SendCommand(transactionCreateCommand);                    
                }
            }
            //Si el proceso es correcto
            return Ok(transaction);
        }
    }
}
