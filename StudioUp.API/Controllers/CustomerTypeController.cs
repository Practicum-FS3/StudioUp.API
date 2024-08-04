using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTypeController : ControllerBase
    {
        private readonly IRepository<CustomerType> _repository;
        private readonly ILogger<CustomerTypeController> _logger;


        public CustomerTypeController(IRepository<CustomerType> repsitory, ILogger<CustomerTypeController> logger)
        {
            _repository = repsitory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerType>>> GetCustomerTypes()
        {
            try
            {
                var customerTypes = await _repository.GetAllAsync();
                return Ok(customerTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerTypeController/GetCustomerTypes");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerType>> GetCustomerType(int id)
        {
            try
            {
                var customerType = await _repository.GetByIdAsync(id);
                if (customerType == null)
                {
                    return NotFound("customer not found by ID");
                }
                return Ok(customerType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerTypeController/GetCustomerType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerType(int id, CustomerType customerType)
        {
            if (customerType == null)
            {
                return BadRequest("The content field is null.");
            }
            if (id != customerType.ID)
            {
                return BadRequest("ID in URL does not match ID in body");
            }
            try
            {
                await _repository.UpdateAsync(customerType);
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, " this error in CustomerTypeController/PutCustomerType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost]
        public async Task<ActionResult<CustomerType>> PostCustomerType(CustomerType customerType)
        {
            if (customerType == null)
            {
                return BadRequest("The content field is null.");
            }
            try
            {
                await _repository.AddAsync(customerType);
                return CreatedAtAction(nameof(GetCustomerType), new { id = customerType.ID }, customerType);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, " this error in CustomerTypeController/PostCustomerType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerType(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerTypeController/DeleteCustomerType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}
