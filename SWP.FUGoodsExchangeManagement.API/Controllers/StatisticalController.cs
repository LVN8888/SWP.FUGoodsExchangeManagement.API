using Microsoft.AspNetCore.Mvc;
using SWP.FUGoodsExchangeManagement.Business.Service.StatisticalServices;
using System.Linq;
using System.Threading.Tasks;


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
        public async Task<IActionResult> GetSummary()
        {
            var summary = await _statisticalService.GetSummaryAsync();
            return Ok(summary);
        }

        [HttpGet("post-mode")]
        public async Task<IActionResult> GetPostModeStatistics(DateTime? startDate, DateTime? endDate)
        {
            var statistics = await _statisticalService.GetPostModeStatisticsAsync(startDate, endDate);
            return Ok(statistics);
        }

        [HttpGet("user-purchase")]
        public async Task<IActionResult> GetUserPurchaseStatistics(DateTime? startDate, DateTime? endDate)
        {
            var statistics = await _statisticalService.GetUserPurchaseStatisticsAsync(startDate, endDate);
            return Ok(statistics);
        }
    }
}
