using Microsoft.AspNetCore.Mvc;
using StudioUp.Models;
using StudioUp.Repo;
using Microsoft.AspNetCore.Http;
using StudioUp.DTO;
using AutoMapper;


namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingTypeController : ControllerBase
    {
       
    
        private readonly IRepository<TrainingTypeDTO> _repository;
        private readonly ILogger<TrainingTypeController> _logger;
        public TrainingTypeController(IRepository<TrainingTypeDTO> repsitory, ILogger<TrainingTypeController> logger)
        {
            _repository = repsitory;
            _logger = logger;   
        }

        [HttpGet("GetTrainingTypes")]
        public async Task<ActionResult<IEnumerable<TrainingTypeDTO>>> GetTrainingTypes()
        {
            try
            {var trainingTypes = await _repository.GetAllAsync();
            return Ok(trainingTypes);

            
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingTypeController/GetTrainingTypes");
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpGet("GetTrainingTypeById/{id}")]
        public async Task<ActionResult<TrainingType>> GetTrainingType(int id)
        {
            var TrainingType = await _repository.GetByIdAsync(id);
            if (TrainingType == null)
            {
                return NotFound();
            }
            return Ok(TrainingType);
        }

        [HttpPut("PutTrainingType")]
        public async Task<IActionResult> PutTrainingType(TrainingTypeDTO TrainingTypeDto)
        {

            await _repository.UpdateAsync(TrainingTypeDto);
            return NoContent();
        }
       
        [HttpPost("PostTrainingType")]
        public async Task<ActionResult<TrainingTypeDTO>> PostTrainingType(TrainingTypeDTO TrainingTypeDto)
        {
          
            if (TrainingTypeDto == null)
            {
                return BadRequest("The trainingType option field is null.");
            }
            try
            {
                var p = await _repository.AddAsync(TrainingTypeDto);
                return Ok(p);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingTypeController/PostTrainingType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteTrainingTypeById{id}")]
        public async Task<IActionResult> DeleteTrainingType(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingTypeController/DeleteTrainingType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

