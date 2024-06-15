using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.FUGoodsExchangeManagement.Business.Service.CategoryServices;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.CategoryDTOs.RequestModel;

namespace SWP.FUGoodsExchangeManagement.API.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? page,
                                       [FromQuery] string? search) 
        {
            var list = await _categoryService.GetAll(page, search);
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryCreateRequestModel model)
        {
            await _categoryService.Add(model);
            return Ok("Add category successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateRequestModel model)
        {
            await _categoryService.Update(model);
            return Ok("Update successfully");
        }


    }
}
