using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HMOController : ControllerBase
    {
        readonly IHMORepository HMOService;
        private readonly ILogger<HMOController> _logger;


        public HMOController(IHMORepository HMOService, ILogger<HMOController> logger)
        {
            this.HMOService = HMOService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<HMODTO>>> GetAll()
        {
            try
            {
                var HMO = await HMOService.GetAllAsync();
                return Ok(HMO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in HMOController/GetAll");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("AddHMO")]
        public async Task<ActionResult<HMODTO>> AddHMO(HMODTO hmo)
        {
            if (hmo == null)
            {
                return BadRequest("The content field is null.");
            }
            try
            {
                var HMO = await HMOService.AddAsync(hmo);
                return Ok(HMO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in HMOController/add");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteHMO(int id)
        {
            try
            {

                await HMOService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, " this error in HMOController/delete");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetHMOById/{id}")]
        public async Task<ActionResult<HMODTO>> GetHMOById(int id)
        {
            try
            {
                var HMO = await HMOService.GetByIdAsync(id);
                if (HMO == null)
                {
                    return NotFound("content hmo not found by ID");

                }
                return Ok(HMO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in HMOController/getById");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateHMO")]
        public async Task<IActionResult> UpdateHMO(HMODTO hmo)
        {
            if (hmo == null)
            {
                return BadRequest("The hmo field is null.");
            }
            try
            {
                await HMOService.UpdateAsync(hmo);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in HMOController/update");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

    }
}
