using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioUp.Models;
using StudioUp.Repo;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTypeController : ControllerBase
    {
        private readonly IRepository<CustomerType> _repository;

        public CustomerTypeController(IRepository<CustomerType> repsitory)
        {
            _repository = repsitory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerType>>> GetCustomerTypes()
        {
            var customerTypes = await _repository.GetAllAsync();
            return Ok(customerTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerType>> GetCustomerType(int id)
        {
            var customerType = await _repository.GetByIdAsync(id);
            if (customerType == null)
            {
                return NotFound();
            }
            return Ok(customerType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerType(int id, CustomerType customerType)
        {
            if (id != customerType.ID)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(customerType);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CustomerType>> PostCustomerType(CustomerType customerType)
        {
            await _repository.AddAsync(customerType);
            return CreatedAtAction(nameof(GetCustomerType), new { id = customerType.ID }, customerType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerType(int id)
        {
            try
            {
                var customerType = await _repository.GetByIdAsync(id);
                if (customerType == null)
                {
                    return NotFound($"Training with ID {id} not found.");
                }

                customerType.IsActive = false;
                await _repository.UpdateAsync(customerType);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
