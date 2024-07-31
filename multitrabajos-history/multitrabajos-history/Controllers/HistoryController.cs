using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multitrabajos_history.DTOs;
using multitrabajos_history.Services;

namespace multitrabajos_history.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IServiceHistory _historyService;
        public HistoryController(IServiceHistory historyService)
        {
            _historyService = historyService;
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> Get(int accountId)
        {
            IEnumerable<HistoryResponse> model = null;

            var data = await _historyService.GetAll();
            model = data.Where(x => x.AccountId == accountId).ToList();

            return Ok(model);
        }
    }
}
