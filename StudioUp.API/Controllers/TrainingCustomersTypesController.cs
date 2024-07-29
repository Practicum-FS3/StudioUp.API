using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo;
using StudioUp.Repo.IRepositories;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingCustomersTypesController : Controller
    {
        private readonly ITrainingCustomerTypeRepository _repository;

        public TrainingCustomersTypesController(ITrainingCustomerTypeRepository repsitory)
        {
            _repository = repsitory;
        }

        //גם כאלו שהם לא בפעילות TrainingCusromerType פונקציה שמביאה את כל המערך של
        [HttpGet("allTCT")]
        public async Task<ActionResult<IEnumerable<TrainingCustomerType>>> GetTrainingCustomerType()
        {
            var trainingCustomerType = await _repository.GetAllTrainingCustomerTypes();
            return Ok(trainingCustomerType);
        }

        //אבל רק את אלו שבפעילות TrainingCusromerType פונקציה שמביאה את המערך של
        [HttpGet("TCTInActivity")]
        public async Task<ActionResult<IEnumerable<TrainingCustomerType>>> GetTrainingCustomerTypeInActivity()
        {
            var trainingCustomerType = await _repository.GetActiveTrainingCustomerTypes();
            return Ok(trainingCustomerType);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingCustomerType>> GetTrainingCustomerType(int id)
        {
            var TrainingCustomerType = await _repository.GetTrainingCustomerTypeById(id);
            if (TrainingCustomerType == null)
            {
                return NotFound();
            }
            return Ok(TrainingCustomerType);
        }

        //הפונקציה לא מוחקת בפועל את השיעור אלא רק הופכת את ה isActive להיות false
        [HttpPut("delete/{id}")]
        public async Task<IActionResult> DeleteTrainingCustomerType(int id)
        {
            await _repository.DeleteTrainingCustomerType(id);
            return Ok();
        }

        //עדכון רגיל של אימון
        [HttpPut("{id}")]
        public async Task<ActionResult<TrainingCustomerTypeDTO>> UpdateTrainingCustomerType(int id, TrainingCustomerTypeDTO trainingCustomerTypedto)
        {
            if (id != trainingCustomerTypedto.ID)
            {
                return BadRequest();
            }
            await _repository.UpdateTrainingCustomerType(id,trainingCustomerTypedto);
            return Ok(trainingCustomerTypedto);
        }

        [HttpPost]
        public async Task<ActionResult<TrainingCustomerTypeDTO>> addTrainingCustomerType(TrainingCustomerTypeDTO trainingCustomerTypedto)
        {
            await _repository.AddTrainingCustomerType(trainingCustomerTypedto);
            return CreatedAtAction(nameof(GetTrainingCustomerType), new { id = trainingCustomerTypedto.ID }, trainingCustomerTypedto);
        }


    }
}
