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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvailableTrainingDTO>>> GetAvailableTrainings()
        {
            var availableTrainingsDTO = await _availableTrainingRepository.GetAllAvailableTrainingsAsync();
            return Ok(availableTrainingsDTO);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AvailableTrainingDTO>> GetAvailableTraining(int id)
        {
            var availableTrainingDTO = await _availableTrainingRepository.GetAvailableTrainingByIdAsync(id);

            if (availableTrainingDTO == null)
            {
                return NotFound();
            }

            return Ok(availableTrainingDTO);
        }
        [HttpPost]
        public async Task<ActionResult<AvailableTrainingDTO>> CreateAvailableTraining(AvailableTrainingDTO availableTrainingDTO)
        {
            await _availableTrainingRepository.AddAvailableTrainingAsync(availableTrainingDTO);
            return CreatedAtAction(nameof(GetAvailableTraining), new { id = availableTrainingDTO.Id }, availableTrainingDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAvailableTraining(int id, AvailableTrainingDTO availableTrainingDTO)
        {
            if (id != availableTrainingDTO.Id)
            {
                return BadRequest();
            }

            await _availableTrainingRepository.UpdateAvailableTrainingAsync(availableTrainingDTO);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvailableTraining(int id)
        {
            await _availableTrainingRepository.DeleteAvailableTrainingAsync(id);
            return NoContent();
        }

    }
}
