using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerControllers : ControllerBase
    {
        private readonly ITrainerRepository trainerRepository;
        private readonly ILogger<TrainerControllers> _logger;


        public TrainerControllers(ITrainerRepository trainerRepository, ILogger<TrainerControllers> logger)
        {
            this.trainerRepository = trainerRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("/getAllTrainers")]
        public async Task<ActionResult<List<TrainerDTO>>> getAllTrainers()
        {
            try
            {
                var trainer = await trainerRepository.GetAllTrainers();
                return Ok(trainer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainerControllers/getAllTrainers");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("/getTrainerById/{id}")]
        public async Task<ActionResult<TrainerDTO>> getTrainerById(int id)
        {
            try
            {
                var trainer = await trainerRepository.GetTrainerById(id);
                if (trainer == null)
                {
                    return NotFound("customer not found by ID");
                }
                return Ok(trainer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainerControllers/getTrainerById");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("/addTrainer")]
        public async Task<ActionResult<TrainerDTO>> addTrainer(TrainerDTO t)
        {
            if (t == null)
            {
                return BadRequest("The trainer field is null.");
            }
            try
            {
                var trainer = await trainerRepository.AddTrainer(t);
                return Ok(trainer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainerControllers/addTrainer");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpPut]
        [Route("/updateTrainer")]
        public async Task<IActionResult> updateTrainer(DTO.TrainerDTO t)

        {
            if (t == null)
            {
                return BadRequest("The trainer field is null.");
            }
            try
            {
                 await trainerRepository.UpdateTrainer(t);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainerControllers/updateTrainer");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete]
        [Route("/deleteTrainer/{id}")]
        public async Task<IActionResult> deleteTrainer(int id)
        {
            try
            {
                await trainerRepository.DeleteTrainer(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainerControllers/deleteTrainer");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}


