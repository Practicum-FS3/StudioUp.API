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

        [HttpGet("GetCustomerTrainingsByCustomerIdAsync/{id}")]
        public async Task<ActionResult<IEnumerable<CalanderAvailableTrainingDTO>>> GetCustomerTrainingsByCustomerIdAsync(int id)
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
        [HttpGet("GetAllCustomerTrainingsAsync")]
        public async Task<ActionResult<IEnumerable<CalanderAvailableTrainingDTO>>> GetAllCustomerTrainingsAsync()
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

        [HttpGet("filterCustomers")]
        public async Task<ActionResult<List<CalanderAvailableTrainingDTO>>> FilterCustomers([FromQuery] DTO.CalanderAvailableTrainingFilterDTO filter)
        {
            try
            {
                return Ok(await _repository.FilterAsync(filter));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CustomerTrainingsDetails/FilterTrainings");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
