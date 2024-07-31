using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multritrabajos_withdrawal.DTOs;
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
        public WithdrawalController(IServiceWithdrawal serviceWithdrawal, IServiceAccount serviceAccount, IServiceNotification serviceNotification)
        {
            _serviceWithdrawal = serviceWithdrawal;
            _serviceAccount = serviceAccount;
            _serviceNotification = serviceNotification;
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
            withdrawal = await _serviceWithdrawal.WithDrawal(withdrawal);

            bool isProcessed = _serviceAccount.Execute(withdrawal);
            bool complete = _serviceNotification.Execute(withdrawal);

            return Ok(withdrawal);
        }

    }
}
