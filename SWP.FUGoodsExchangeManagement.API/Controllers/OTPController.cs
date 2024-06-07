using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.FUGoodsExchangeManagement.Business.Service.OTPServices;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.OTPDTOs;

namespace SWP.FUGoodsExchangeManagement.API.Controllers
{
    [ApiController]
    [Route("api/otp-management")]
    public class OTPController : Controller
    {
        private readonly IOTPService _otpService;

        public OTPController(IOTPService oTPService)
        {
            _otpService = oTPService;
        }

        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> SendOTP(OTPSendEmailRequestModel model)
        {
            await _otpService.CreateOTPCodeForEmail(model);
            return Ok();
        }

        [HttpPost]
        [Route("verify")]
        public async Task<IActionResult> VerifyOTP(OTPVerifyRequestModel model)
        {
            await _otpService.VerifyOTP(model);
            return Ok();
        }
    }
}
