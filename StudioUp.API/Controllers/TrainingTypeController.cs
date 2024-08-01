using Microsoft.AspNetCore.Mvc;
using StudioUp.Models;
using StudioUp.Repo;
using Microsoft.AspNetCore.Http;


namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingTypeController : ControllerBase
    {
       
    
        private readonly IRepository<TrainingType> _repository;

        public TrainingTypeController(IRepository<TrainingType> repsitory)
        {
            _repository = repsitory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingType>>> GetTrainingTypes()
        {
            var trainingTypes = await _repository.GetAllAsync();
            return Ok(trainingTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingType>> GetTrainingType(int id)
        {
            var TrainingType = await _repository.GetByIdAsync(id);
            if (TrainingType == null)
            {
                return NotFound();
            }
            return Ok(TrainingType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingType(int id, TrainingType TrainingType)
        {
            if (id != TrainingType.ID)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(TrainingType);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TrainingType>> PostTrainingType(TrainingType TrainingType)
        {
            await _repository.AddAsync(TrainingType);
            return CreatedAtAction(nameof(GetTrainingType), new { id = TrainingType.ID }, TrainingType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingType(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}

