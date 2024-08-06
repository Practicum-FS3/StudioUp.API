using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Repo.IRepositories;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HMOController : ControllerBase
    {
        private readonly IHMORepository _hmoService;
        private readonly ILogger<HMOController> _logger;

        public HMOController(IHMORepository hmoService, ILogger<HMOController> logger)
        {
            _hmoService = hmoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<HMODTO>>> GetAll()
        {
            try
            {
                var hmoList = await _hmoService.GetAllAsync();
                return Ok(hmoList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAll method.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<HMODTO>> Add(HMODTO hmo)
        {
            if (hmo == null)
            {
                return BadRequest("The HMO field is null.");
            }
            try
            {
                var newHMO = await _hmoService.AddAsync(hmo);
                return CreatedAtAction(nameof(GetById), new { id = newHMO.ID }, newHMO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Add method.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _hmoService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Delete method.");
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<HMODTO>> GetById(int id)
        {
            try
            {
                var hmo = await _hmoService.GetByIdAsync(id);
                if (hmo == null)
                {
                    return NotFound("HMO not found.");
                }
                return Ok(hmo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetById method.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(HMODTO hmo)
        {
            if (hmo == null)
            {
                return BadRequest("The HMO field is null.");
            }
            try
            {
                var isUpdated = await _hmoService.UpdateAsync(hmo);
                if (!isUpdated)
                {
                    return NotFound("HMO not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Update method.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
