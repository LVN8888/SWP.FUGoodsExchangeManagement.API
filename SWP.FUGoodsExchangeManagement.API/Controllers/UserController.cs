using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWP.FUGoodsExchangeManagement.Business.Service.UserServices;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.RequestModels;

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
        public async Task<IActionResult> GetUserList([FromQuery] int? page,
                                                     [FromQuery] string? search,
                                                     [FromQuery] string? sort,
                                                     [FromQuery] string? userRole)
        {
            var list = await _userService.GetUserList(page, search, sort, userRole);
            return Ok(list);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPut]
        [Route("password")]
        public async Task<IActionResult> ChangePassword(UserChangePasswordRequestModel request)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")[1];
            await _userService.ChangePassword(request, token);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("activate-user/{userId}")]
        public async Task<IActionResult> ActivateUser(string userId)
        {
            await _userService.ActivateUser(userId);
            return Ok(new { message = "User activated successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("deactivate-user/{userId}")]
        public async Task<IActionResult> DeactivateUser(string userId)
        {
            await _userService.DeactivateUser(userId);
            return Ok( new { message = "User deactivated successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("edit-user")]
        public async Task<IActionResult> EditUser([FromBody] UserEditRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.EditUser(request);
            return Ok( new { message = "User details updated successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("change-role")]
        public async Task<IActionResult> ChangeUserRole([FromBody] UserRoleChangeRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.ChangeUserRole(request);
            return Ok(new { message = "User role updated successfully" });
        }

    }
}
