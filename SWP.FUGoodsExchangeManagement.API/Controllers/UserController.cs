﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.RequestModels;
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

        [Authorize(Roles = "User, Admin")]
        [HttpPut]
        [Route("password")]
        public async Task<IActionResult> ChangePassword(UserChangePasswordRequestModel request)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")[1];
            await _userService.ChangePassword(request, token);
            return Ok();
        }
    }
}
