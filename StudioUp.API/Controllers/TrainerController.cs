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

        public TrainerControllers(ITrainerRepository trainerRepository)
        {
            this.trainerRepository = trainerRepository;
        }

        [HttpGet]
        [Route("getAllTrainers")]
        public async Task<List<DTO.TrainerDTO>> getAllTrainers()
        {
            try
            {
                return await trainerRepository.GetAllTrainers();
            }
            catch (Exception e)
            {
                throw new Exception("failed to get all trainers");
            }
        }

        [HttpPost]
        [Route("addTrainer")]
        public async Task<TrainerDTO> addTrainer(DTO.TrainerDTO trainer)
        {
            try
            {
                return await trainerRepository.AddTrainer(trainer);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [HttpDelete]
        [Route("deleteTrainer/{id}")]
        public async Task<bool> deleteTrainer(int id)
        [Route("/deleteTrainer/{id}")]
        public async Task<IActionResult> deleteTrainer(int id)
        {
            try
            {
                var trainer = await trainerRepository.GetTrainerById(id);
                if (trainer == null)
                {
                    return NotFound($"Training with ID {id} not found.");
                }

                trainer.IsActive = false;
                await trainerRepository.UpdateTrainer(trainer);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getTrainerById/{id}")]
        public async Task<DTO.TrainerDTO> getTrainerById(int id)
        {
            try
            {
                return await trainerRepository.GetTrainerById(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut]
        [Route("updateTrainer")]
        public async Task<bool> updateTrainer(DTO.TrainerDTO trainer)
        {
            try
            {
                return await trainerRepository.UpdateTrainer(trainer);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}


