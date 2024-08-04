using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly ILogger<TrainingController> _logger;


        public TrainingController(ITrainingRepository trainingRepository, ILogger<TrainingController> logger)
        {
            _trainingRepository = trainingRepository;
            _logger = logger;
        }

        // GET: api/Training
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> Get()
        {
            try
            {
                var trainings = await _trainingRepository.GetAllTrainings();
                return Ok(trainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/Get");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        // GET: api/Training/forCalander

        [HttpGet("forCalander")]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetTrainingsCalender()
        {
            try
            {
                var trainings = await _trainingRepository.GetAllTrainingsCalender();
                return Ok(trainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/GetTrainingsCalender");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Training/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingDTO>> GetById(int id)
        {
            try
            {
                var training = await _trainingRepository.GetTrainingById(id);
                if (training == null)
                {
                    return NotFound("training not found by ID");
                }
                return Ok(training);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/GetById");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Training
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TrainingDTO trainingDTO)
        {
            if (trainingDTO == null)
            {
                return BadRequest("The training field is null.");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _trainingRepository.AddTraining(trainingDTO);
                return CreatedAtAction(nameof(Get), new { id = trainingDTO.ID }, trainingDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/Post");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        // PUT: api/Training/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TrainingDTO trainingDto)
        {

            if (trainingDto == null)
            {
                return BadRequest("The trainingDto field is null.");
            }
            if (id != trainingDto.ID)
            {
                return BadRequest("ID in URL does not match ID in body");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var training = await _trainingRepository.GetTrainingById(id);
                await _trainingRepository.UpdateTraining(trainingDto, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/Put");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        // DELETE: api/Training/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var training = await _trainingRepository.GetTrainingById(id);
                await _trainingRepository.DeleteTraining(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/Delete");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
