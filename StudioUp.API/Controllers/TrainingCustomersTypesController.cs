using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingCustomerType>>> GetTrainingCustomerType()
        {
            var trainingCustomerType = await _repository.GetAllTrainingCustomerTypes();
            return Ok(trainingCustomerType);
        }

    }
}
