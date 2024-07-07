using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.FUGoodsExchangeManagement.Business.Service.PostModeServices;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.PostModeDTOs;
using SWP.FUGoodsExchangeManagement.Repository.Repository.PostModeRepositories;

namespace SWP.FUGoodsExchangeManagement.API.Controllers
{
    [Route("api/post-mode")]
    [ApiController]
    public class PostModeController : ControllerBase
    {
        private readonly IPostModeService _postModeService;
        public PostModeController(IPostModeService postModeService)
        {
            _postModeService = postModeService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateNewPostMode(PostModeAddRequestModel requestModel)
        {
            await _postModeService.AddPostMode(requestModel);
            return Ok("Create successfully");
        }
    }
}
