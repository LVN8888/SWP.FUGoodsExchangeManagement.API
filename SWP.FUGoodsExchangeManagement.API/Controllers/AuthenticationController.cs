using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.Service.UserServices;

namespace SWP.FUGoodsExchangeManagement.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginRequestModel request)
        {
            var user =  await _userService.CheckLogin(request);
            return Ok(user);
        }

        [HttpPost]
        [Route("register-test")]
        public async Task<IActionResult> Register(UserRegisterRequestModel request)
        {
            await _userService.Register(request);
            return Ok("Register successfully!");
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAccount(UserRegisterRequestModelVer1 request)
        {
            await _userService.RegisterAccount(request);
            return Ok("Register successfully!");
        }

        [HttpPost]
        [Route("password/reset")]
        public async Task<IActionResult> ResetPassword(UserResetPasswordRequestModel request)
        {
            await _userService.ResetPassword(request);
            return Ok();
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [Route("password/change")]
        public async Task<IActionResult> ChangePassword(UserChangePasswordRequestModel request)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")[1];
            await _userService.ChangePassword(request, token);
            return Ok();
        }
    }
}
