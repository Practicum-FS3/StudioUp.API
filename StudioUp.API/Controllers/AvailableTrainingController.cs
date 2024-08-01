using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailableTrainingController : ControllerBase
    {
        private readonly IAvailableTrainingRepository _availableTrainingRepository;
        public AvailableTrainingController(IAvailableTrainingRepository availableTrainingRepository)
        {
            _availableTrainingRepository = availableTrainingRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<AvailableTrainingDTO>>> GetAllAvailableTrainings()
        {
            try
            {
                var availableTrainingsDTO = await _availableTrainingRepository.GetAllAvailableTrainingsAsync();
                return Ok(availableTrainingsDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/AvailableTraining/forCalander

        [HttpGet("forCalander")]
        public async Task<ActionResult<IEnumerable<AvailableTrainingDTO>>> GetAvailableTrainingsCalender()
        {
            var availableTrainingsDTO = await _availableTrainingRepository.GetAllAvailableTrainingsAsyncForCalander();
            return Ok(availableTrainingsDTO);
        }

        //[HttpGet("{id}")]
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<AvailableTrainingDTO>> GetByIdAvailableTraining(int id)
        {
            try
            {
                var availableTrainingDTO = await _availableTrainingRepository.GetAvailableTrainingByIdAsync(id);

                if (availableTrainingDTO == null)
                {
                    return NotFound($"Training with ID {id} not found.");
                }

                return Ok(availableTrainingDTO);
            }catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message} ");
            }
        }
        [HttpPost("Add")]
        public async Task<ActionResult<AvailableTrainingDTO>> CreateAvailableTraining([FromBody] AvailableTrainingDTO availableTrainingDTO)
        {
            if (availableTrainingDTO == null)
            {
                return BadRequest("The availableTrainingDTO field is required.");
            }
            try
            {
                await _availableTrainingRepository.AddAvailableTrainingAsync(availableTrainingDTO);
                return CreatedAtAction(nameof(GetByIdAvailableTraining), new { id = availableTrainingDTO.Id }, availableTrainingDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAvailableTraining(int id, [FromBody] AvailableTrainingDTO availableTrainingDTO)
        {
            if (id != availableTrainingDTO.Id)
            {
                return BadRequest("ID mismatch.");
            }
            try
            {
                await _availableTrainingRepository.UpdateAvailableTrainingAsync(id,availableTrainingDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAvailableTraining(int id)
        {
            try
            {
                var deleted = await _availableTrainingRepository.DeleteAvailableTrainingAsync(id);
                if (!deleted)
                {
                    return NotFound($"Training with ID {id} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            //try
            //{
            //    await _availableTrainingRepository.DeleteAvailableTrainingAsync(id);
            //    return NoContent();
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, $"Internal server error: {ex.Message}");
            //}
        }

    }
}
