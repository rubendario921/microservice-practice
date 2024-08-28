using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace multitrabajos_history.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            _logger.LogInformation("Receive ping ...");
            return Ok();
        }
    }

}
