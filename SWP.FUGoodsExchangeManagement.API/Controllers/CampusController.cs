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
            try
            {
                await _campusService.AddCampusAsync(addCampusDto);
                return Ok("Campus added successfully");
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditCampus([FromBody] EditCampusDTO editCampusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _campusService.EditCampusAsync(editCampusDto);
                return Ok("Campus edited successfully");
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCampus([FromBody] DeleteCampusDTO deleteCampusDTO)
        {
            try
            {
                await _campusService.DeleteCampusAsync(deleteCampusDTO);
                return Ok("Campus deleted successfully");
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCampuses(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var campuses = await _campusService.GetAllCampusesAsync(pageIndex, pageSize);
                return Ok(campuses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
