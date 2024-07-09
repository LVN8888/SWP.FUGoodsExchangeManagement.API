using Microsoft.AspNetCore.Mvc;
using SWP.FUGoodsExchangeManagement.Business.Service.CampusServices;
using SWP.FUGoodsExchangeManagement.Business.Utils;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.CampusDTOs.RequestModels;

namespace SWP.FUGoodsExchangeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampusController : ControllerBase
    {
        private readonly ICampusService _campusService;

        public CampusController(ICampusService campusService)
        {
            _campusService = campusService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCampus([FromBody] AddCampusDTO addCampusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _campusService.AddCampusAsync(addCampusDto);
            return Ok("Campus added successfully");
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditCampus([FromBody] EditCampusDTO editCampusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _campusService.EditCampusAsync(editCampusDto);
            return Ok("Campus edited successfully");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCampus([FromBody] DeleteCampusDTO deleteCampusDTO)
        {
            await _campusService.DeleteCampusAsync(deleteCampusDTO);
            return Ok("Campus deleted successfully");
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCampuses()
        {
            var campuses = await _campusService.GetAllCampusesAsync();
            return Ok(campuses);
        }
    }
}
