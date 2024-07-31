using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multritrabajos_accounts.DTOs;
using multritrabajos_accounts.Services;

namespace multritrabajos_accounts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IServicesAccount _accountService;

        public AccountController(IServicesAccount accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]        
        public async Task<ActionResult> Get()
        {
            try
            {
                var accounts = await _accountService.GetAll();
                return Ok(accounts);
            }
            catch (Exception ex) { 
                return StatusCode(StatusCodes.Status500InternalServerError, new {message = "Error occurred while fetching the accounts.", detail = ex.Message});
            }            
        }

        [HttpPost("Deposit")]
        public async Task<ActionResult> Deposit(AccountRequest request)
        {
            var result = await _accountService.GetbyId(request.IdAccount);
            Models.Account account = new Models.Account()
            {
                IdAccount = request.IdAccount,
                IdCustomer = result.IdCustomer,
                TotalAmount = result.TotalAmount + request.Amount,
                Customer = result.Customer
            };
            await _accountService.Deposit(account);
            return Ok();
        }
        [HttpPost("WithDrawal")]
        public async Task<ActionResult> WithDrawal(AccountRequest request)
        {
            var result = await _accountService.GetbyId(request.IdAccount);
            if (result.TotalAmount < request.Amount)
            {
                return BadRequest(new { message = "No hay fondos para realizar el retiro" });
            }
            Models.Account account = new Models.Account()
            {
                IdAccount = request.IdAccount,
                IdCustomer = result.IdCustomer,
                TotalAmount = result.TotalAmount - request.Amount,
                Customer = result.Customer
            };
            await _accountService.Deposit(account);
            return Ok();
        }
    }
}
