using Microsoft.AspNetCore.Mvc;
using SWP.FUGoodsExchangeManagement.Business.Service.ReportServices;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.ReportDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.ReportDTOs.ResponseModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportRequestModel request)
        {
            await _reportService.CreateReportAsync(request.ProductPostId, request.CreatedBy, request.Reasons);
            return Ok("Report created successfully.");
        }

        [HttpPost("approve")]
        public async Task<IActionResult> ApproveReport([FromBody] ApproveReportRequestModel request)
        {
            await _reportService.ApproveReportAsync(request.ReportId);
            return Ok("Report approved successfully.");
        }

        [HttpPost("decline")]
        public async Task<IActionResult> DeclineReport([FromBody] DeclineReportRequestModel request)
        {
            await _reportService.DeclineReportAsync(request.ReportId);
            return Ok("Report declined successfully.");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _reportService.GetAllReportsAsync();
            var response = reports.Select(r => new ReportResponseModel
            {
                Id = r.Id,
                Content = r.Content,
                Date = r.Date,
                ProductPostId = r.ProductPostId,
                CreatedBy = r.CreatedBy
            });
            return Ok(response);
        }

        [HttpGet("by-product-post/{productPostId}")]
        public async Task<IActionResult> GetReportsByProductPostId(string productPostId)
        {
            var reports = await _reportService.GetReportsByProductPostIdAsync(productPostId);
            var response = reports.Select(r => new ReportResponseModel
            {
                Id = r.Id,
                Content = r.Content,
                Date = r.Date,
                ProductPostId = r.ProductPostId,
                CreatedBy = r.CreatedBy
            });
            return Ok(response);
        }
    }
}
