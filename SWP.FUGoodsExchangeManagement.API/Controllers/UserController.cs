using Microsoft.AspNetCore.Mvc;
using SWP.FUGoodsExchangeManagement.Repository.Service.UserServices;

namespace SWP.FUGoodsExchangeManagement.API.Controllers
{
    [ApiController]
    [Route("api/user-management")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserList([FromQuery] int? page)
        {
            var list = await _userService.GetUserList(page);
            return Ok(list);
        }
    }
}
