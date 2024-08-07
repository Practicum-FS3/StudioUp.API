using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;
using System;
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
        [HttpGet("GetTrainings")]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetTrainings()
        {
            try
            {
                var trainings = await _trainingRepository.GetAllTrainingsAsync();
                return Ok(trainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/Get");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        // GET: api/Training/forCalander

        [HttpGet("GetTrainingsCalender")]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetTrainingsCalender()
        {
            try
            {
                var trainings = await _trainingRepository.GetAllTrainingsCalenderAsync();
                return Ok(trainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/GetTrainingsCalender");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Training/5
        [HttpGet("GetTrainingById/{id}")]
        public async Task<ActionResult<TrainingDTO>> GetTrainingById(int id)
        {
            try
            {
                var training = await _trainingRepository.GetTrainingByIdAsync(id);
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
        /* [HttpPost("")]
         public async Task<IActionResult> PostTraining([FromBody] TrainingDTO trainingDTO)
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

         }*/
        [HttpPost("PostTraining")]
        public async Task<IActionResult> PostTraining(TrainingDTO training)
        {
            if (training == null)
            {
                return BadRequest("The training field is null.");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
               var x= await _trainingRepository.AddTrainingAsync(training);
                return Ok(x);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/Post");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        // PUT: api/Training/5
        [HttpPut("PutTraining")]
        public async Task<IActionResult> PutTraining(TrainingDTO trainingDto)
        {

            if (trainingDto == null)
            {
                return BadRequest("The trainingDto field is null.");
            }
           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var training = await _trainingRepository.GetTrainingByIdAsync(trainingDto.ID);
                await _trainingRepository.UpdateTrainingAsync(trainingDto, trainingDto.ID);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/Put");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Training/5
        [HttpDelete("DeleteTraining/{id}")]
        public async Task<IActionResult> DeleteTraining(int id)
        {
            try
            {
                var training = await _trainingRepository.GetTrainingByIdAsync(id);
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
