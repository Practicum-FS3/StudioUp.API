using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Repo.IRepositories;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerHMOSController : ControllerBase
    {
        private readonly ICustomerHMOSRepository _customerHMOSRepository;
        public CustomerHMOSController(ICustomerHMOSRepository customerHMOSRepository)
        {
            _customerHMOSRepository = customerHMOSRepository;
        }

        [HttpGet("GetCustomerHMOS")]
        public async Task<ActionResult<IEnumerable<CustomerHMOSDTO>>> GetAll()
        {
            try
            {
                var customersHMOs = await _customerHMOSRepository.GetAllAsync();
                return Ok(customersHMOs);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetCustomerHMOSByID/{id}")]
        public async Task<ActionResult<CustomerHMOSDTO>> GetById(int id)
        {
            try
            {
                var customerHMOS = await _customerHMOSRepository.GetByIdAsync(id);
                if (customerHMOS == null)
                    return NotFound($"CustomerHMOS with ID {id} not found.");
                return Ok(customerHMOS);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message} ");
            }
        }
        [HttpPost("CreateCustomerHMOS")]
        public async Task<ActionResult<int>> Create([FromBody] CustomerHMOSDTO customerHMOSDTO)
        {
            if (customerHMOSDTO == null)
                return BadRequest();

            var newCustomerId = await _customerHMOSRepository.AddAsync(customerHMOSDTO);
            return CreatedAtAction(nameof(GetById), new { id = newCustomerId }, newCustomerId);
        }

        [HttpPut("UpdateCustomerHMOS/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerHMOSDTO customerHMOSDTO)
        {
            if (customerHMOSDTO == null)
                return BadRequest();

            await _customerHMOSRepository.UpdateAsync(id, customerHMOSDTO);
            return NoContent();
        }

        [HttpDelete("DeleteCustomerHMOS/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _customerHMOSRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

    }
}
