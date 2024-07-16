using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using StudioUp.DTO;
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

        public TrainingController(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        // GET: api/Training
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> Get()
        {
            var trainings = await _trainingRepository.GetAllTrainings();
            return Ok(trainings);
        }

        // GET: api/Training/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingDTO>> Get(int id)
        {
            var training = await _trainingRepository.GetTrainingById(id);
            if (training == null)
            {
                return NotFound();
            }
            return Ok(training);
        }

        // POST: api/Training
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TrainingDTO trainingDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _trainingRepository.AddTraining(trainingDTO);
            return CreatedAtAction(nameof(Get), new { id = trainingDTO.ID }, trainingDTO);
        }

        // PUT: api/Training/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TrainingDTO trainingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var training = await _trainingRepository.GetTrainingById(id);

            if (training == null)
                return NotFound();

            await _trainingRepository.UpdateTraining(trainingDto);
            return NoContent();
        }

        // DELETE: api/Training/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var training = await _trainingRepository.GetTrainingById(id);
            if (training == null)
                return NotFound();
            await _trainingRepository.DeleteTraining(id);
            return NoContent();
        }
    }
}
