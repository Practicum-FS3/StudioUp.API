using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Repo;
using StudioUp.Repo.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTrainingsDetailsController : ControllerBase
    {
        private readonly CustomerTrainingsDetailsRepository _repository;
        private readonly ILogger<CustomerTrainingsDetailsController> _logger;

        public CustomerTrainingsDetailsController(CustomerTrainingsDetailsRepository repository, ILogger<CustomerTrainingsDetailsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CalanderAvailableTrainingDTO>>> GetByCystomerIdAsync(int id)
        {
            try
            {
                var trainings = await _repository.GetDetailsForCustomerAsync(id);
                return Ok(trainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in AvailableTrainingController/GetAvailableTrainingsForCustomer");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalanderAvailableTrainingDTO>>> GetAllAsync()
        {
            try
            {
                var trainings = await _repository.GetAllCustomersDetailsAsync();
                return Ok(trainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in AvailableTrainingController/GetAvailableTrainingsForCustomer");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<CalanderAvailableTrainingDTO>>> FilterCustomers(DTO.CalanderAvailableTrainingFilterDTO filter)
        {
            try
            {
                return Ok(await _repository.FilterAsync(filter));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerTrainingsDeatails/FilterTrainings");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
