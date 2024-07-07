using Microsoft.AspNetCore.Mvc;
using SWP.FUGoodsExchangeManagement.Business.Service.StatisticalServices;

namespace SWP.FUGoodsExchangeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly IStatisticalService _statisticalService;

        public StatisticalController(IStatisticalService statisticalService)
        {
            _statisticalService = statisticalService;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummaryAsync()
        {
            return Ok(await _statisticalService.GetSummaryAsync());
        }
    }
}
