﻿//using Microsoft.AspNetCore.Mvc;
//using StudioUp.DTO;
////using StudioUp.Models;
//using StudioUp.Repo.IRepositories;
//using StudioUp.Repo.Repositories;

//namespace StudioUp.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CustomerController : ControllerBase
//    {
//        readonly ICustomerRepository _customerService;
//        private readonly ILogger<CustomerController> _logger;


//        public CustomerController(ICustomerRepository customerService, ILogger<CustomerController> logger)
//        {
//            _customerService = customerService;
//            _logger = logger;
//        }

//        [HttpGet]
//        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomer()
//        {
//            try
//            {
//                var c = await _customerService.GetAllAsync();
//                return Ok(c);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, " this error in CustomerController/GetAllCustomer");
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        [HttpGet("byId/{id}")]

//        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int id)
//        {
//            try
//            {
//                var c = await _customerService.GetByIdAsync(id);
//                if (c == null)
//                {
//                    return NotFound("customer not found by ID");
//                }
//                return Ok(c);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, " this error in CustomerController/GetCustomerById");
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }


//        [HttpPost]
//        [Route("addCustomer")]
//        public async Task<ActionResult<CustomerDTO>> AddCustomer(CustomerDTO customer)
//        {
//             try
//            {
//                var c = await _customerService.AddAsync(customer);
//                if (c == null)
//                {
//                    return BadRequest("customer field is null.");
//                }
//                return Ok(c);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, " this error in CustomerController/AddCustomer");
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        [HttpPut]
//        public async Task<IActionResult> UpdateCustomer(CustomerDTO customer)
//        {
//            if (customer == null)
//            {
//                return BadRequest("The content  field is null.");
//            }
//            try
//            {
//                 await _customerService.UpdateAsync(customer);
//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, " this error in CustomerController/UpdateCustomer");
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteCustomer(int id)
//        {
//            try
//            {
//                 await _customerService.DeleteAsync(id);
//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, " this error in CustomerController/DeleteCustomer");
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }
//        [HttpGet("filter")]
//        public async Task<ActionResult<List<CustomerDTO>>> FilterCustomers([FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] string? email)
//        {
//            try
//            {
//                filter.FirstName = filter.FirstName?.Trim();
//                filter.LastName = filter.LastName?.Trim();
//                filter.Email = filter.Email?.Trim();

//                return Ok(await _customerService.FilterAsync(filter));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, " this error in CustomerController/FilterCustomers");
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }




//    }
//}
