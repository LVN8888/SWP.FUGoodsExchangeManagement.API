using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.FUGoodsExchangeManagement.Business.Service.PaymentServices;
using SWP.FUGoodsExchangeManagement.Business.Service.ProductPostServices;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.PaymentDTOs.Request;
using SWP.FUGoodsExchangeManagement.Repository.Enums;
using SWP.FUGoodsExchangeManagement.Repository.Models;

namespace SWP.FUGoodsExchangeManagement.API.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IProductPostService _productPostService;

        public PaymentController(IPaymentService paymentService, IProductPostService productPostService)
        {
            _paymentService = paymentService;
            _productPostService = productPostService;
        }

        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdatePaymentStatus(PaymentUpdateRequestModel model)
        {
            var currentPayemt = await _paymentService.UpdatePaymentStatus(model);

            if (model.Status.Equals(nameof(PaymentStatus.Success)))
            {
                await _productPostService.ExtendExpiredDateAfterPaymentSuccess(currentPayemt.ProductPostId, currentPayemt.PostModeId);
                return Ok("Update successfully, post duration has been extended");
            }

            return Ok("Update payment successfully, post duration has not been extended due to an error");
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("payment-url")]
        public async Task<IActionResult> GetPaymentUrl([FromQuery] string paymentId, [FromQuery] string redirectUrl)
        {
            var paymentUrl = await _paymentService.GetPaymentUrl(HttpContext, paymentId, redirectUrl);

            return Ok(paymentUrl);
        }
    }
}
