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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPostMode()
        {
            var response = await _postModeService.ShowPostModeListForAdmin();
            return Ok(response);
        }

        [HttpGet]
        [Route("active")]
        public async Task<IActionResult> GetAllPostModeForUser()
        {
            var response = await _postModeService.ShowPostModeListForUser();
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePostMode(string id, PostModeUpdateModel updateModel)
        {
            await _postModeService.UpdatePostMode(id, updateModel);
            return Ok("Update successfully");
        }

        [HttpPut]
        [Route("{id}/inactive")]
        public async Task<IActionResult> DeactivatePostMode(string id)
        {
            await _postModeService.SoftRemovePostMode(id);
            return Ok("Deactivate successfully");
        }
    }
}
