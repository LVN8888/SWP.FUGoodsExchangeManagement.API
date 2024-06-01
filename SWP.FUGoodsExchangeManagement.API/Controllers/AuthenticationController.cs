using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs;
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
        public async Task<UserLoginResponseModel> Login(UserLoginRequestModel request)
        {
            var user =  await _userService.CheckLogin(request);
            return user;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegisterRequestModel request)
        {
            await _userService.Register(request);
            return Ok("Register successfully!");
        }
    }
}
