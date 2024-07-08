using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.FUGoodsExchangeManagement.Business.Service.ProductPostServices;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.ProductPostDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.ProductPostDTOs.ResponseModels;

namespace SWP.FUGoodsExchangeManagement.API.Controllers
{
    [Route("api/product-post")]
    [ApiController]
    public class ProductPostController : ControllerBase
    {
        private readonly IProductPostService _productPostService;
        public ProductPostController(IProductPostService productPostService)
        {
            _productPostService = productPostService;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateProductPost(ProductPostCreateRequestModel requestModel)
        {
            string token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            await _productPostService.CreateWaitingProductPost(requestModel, token);
            return Ok("Create post successfully. Please wait for moderator approving your post");
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> GetAllProductPost(int? pageIndex, [FromQuery] PostSearchModel searchModel, string status)
        {
            var result = await _productPostService.ViewAllPostWithStatus(pageIndex, searchModel, status);
            return Ok(result);
        }

        [HttpGet]
        [Route("me")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetOwnProductPost(int? pageIndex, [FromQuery] PostSearchModel searchModel, string status)
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var result = await _productPostService.ViewOwnPostWithStatus(pageIndex, searchModel, status, token);
            return Ok(result);
        }

        [HttpGet]
        [Route("others")]
        public async Task<IActionResult> GetOthersProductPost(int? pageIndex, [FromQuery] PostSearchModel searchModel)
        {
            var authorize = Request.Headers["Authorization"].ToString().Split(" ");
            var result = new List<ProductPostResponseModel>();
            if (authorize.Length > 1)
            {
                var token = authorize[1];
                result = await _productPostService.ViewOwnPostExceptMine(pageIndex, searchModel, token);
            }
            else
            {
                result = await _productPostService.ViewOwnPostExceptMine(pageIndex, searchModel, null);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDetailsOfPost(string id)
        {
            var response = await _productPostService.ViewDetailsOfPost(id);
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateProductPost(string id, ProductPostUpdateRequestModel requestModel)
        {
            await _productPostService.UpdateProductPost(id, requestModel);
            return Ok("Update successfully");
        }

        [HttpPut]
        [Route("close/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CloseProductPost(string id)
        {
            await _productPostService.CloseProductPost(id);
            return Ok("Close product post successfully");
        }

        [HttpPut]
        [Route("extend/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ExtendProductPost(string id, [FromBody] string postModeId)
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            await _productPostService.ExtendExpiredDate(id, postModeId, token);
            return Ok("Update successfully");
        }

        [HttpPut]
        [Route("approve/{id}")]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> ApproveProductPost([FromBody] string status, string id)
        {
            await _productPostService.ApprovePost(status, id);
            return Ok("Approve post successfully");
        }
    }
}
