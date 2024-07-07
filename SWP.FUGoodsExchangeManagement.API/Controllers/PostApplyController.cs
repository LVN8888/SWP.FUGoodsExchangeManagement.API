using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.FUGoodsExchangeManagement.Business.Service.PostApplyServices;
using SWP.FUGoodsExchangeManagement.Repository.Repository.PostApplyRepositories;

namespace SWP.FUGoodsExchangeManagement.API.Controllers
{
    [Route("api/post-apply")]
    [ApiController]
    public class PostApplyController : ControllerBase
    {
        private readonly IPostApplyService _postApplyService;
        public PostApplyController(IPostApplyService postApplyService)
        {
            _postApplyService = postApplyService;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("{postId}")]
        public async Task<IActionResult> BuyProduct([FromBody] string? message, string postId)
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            await _postApplyService.BuyProduct(message, postId, token);
            return Ok("Apply to buy product in post sucessfully");
        }

        [HttpDelete]
        [Authorize(Roles = "User")]
        [Route("{id}")]
        public async Task<IActionResult> DeleteApply(string id)
        {
            await _postApplyService.DeleteApplyPost(id);
            return Ok("Delete apply successfully");
        }
    }
}
