using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerSubscriptionsController : ControllerBase
    {
        private readonly ICustomerSubscriptionRepository _repository;
        private readonly ILogger<CustomerSubscriptionsController> _logger;

        public CustomerSubscriptionsController(ICustomerSubscriptionRepository repository, ILogger<CustomerSubscriptionsController> logger)
        {
            _repository = repository;
            _logger= logger;    
        }
        [HttpGet("GetAllCustomerSubscriptions")]
        public async Task<ActionResult<IEnumerable<CustomerSubscriptionDTO>>> GetAllCustomerSubscriptions()
        {
            try
            {
                var subscriptions = await _repository.GetAllCustomerSubscriptionsAsync();
                return Ok(subscriptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerSubscriptionsController/GetAllCustomerSubscriptions");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<CustomerSubscriptionDTO>>> GetCustomerSubscriptionsByCustomerId(int customerId)
        {
            try
            {
                var subscriptions = await _repository.GetCustomerSubscriptionsByCustomerIdAsync(customerId);
                if (subscriptions == null || !subscriptions.Any())
                {
                    return NotFound();
                }
                return Ok(subscriptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerSubscriptionsController/GetCustomerSubscriptionsByCustomerId");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetCustomerSubscriptionsByCustomerId/{customerId}")]
        public async Task<ActionResult<IEnumerable<CustomerSubscriptionDTO>>> GetCustomerSubscriptionsByCustomerId(int customerId)
        {
            try
            {
                var subscriptions = await _repository.GetCustomerSubscriptionsByCustomerIdAsync(customerId);
                if (subscriptions == null || !subscriptions.Any())
                {
                    return NotFound();
                }
                return Ok(subscriptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerSubscriptionsController/GetCustomerSubscriptionsByCustomerId");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetCustomerSubscriptionById/{id}")]
        public async Task<ActionResult<CustomerSubscriptionDTO>> GetCustomerSubscriptionById(int id)
        {
            try
            {
                var subscription = await _repository.GetCustomerSubscriptionByIdAsync(id);
                if (subscription == null)
                {
                    return NotFound();
                }
                return Ok(subscription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerSubscriptionsController/GetCustomerSubscriptionById");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
            [HttpPost("AddCustomerSubscription")]
        public async Task<ActionResult> AddCustomerSubscription(CustomerSubscriptionDTO subscriptionDTO)
        {
            try
            {
                var subscription = new CustomerSubscription
                {
                    CustomerID = subscriptionDTO.CustomerID,
                    SubscriptionTypeId = subscriptionDTO.SubscriptionTypeId,
                    StartDate = subscriptionDTO.StartDate
                };
                await _repository.AddCustomerSubscriptionAsync(subscription);
                return CreatedAtAction(nameof(GetCustomerSubscriptionById), new { id = subscription.ID }, subscription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerSubscriptionsController/AddCustomerSubscription");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("UpdateCustomerSubscription")]
        public async Task<IActionResult> UpdateCustomerSubscription(CustomerSubscriptionDTO subscriptionDTO)
        {
            var subscription = new CustomerSubscription
            {
                ID = subscriptionDTO.ID,
                CustomerID = subscriptionDTO.CustomerID,
                SubscriptionTypeId = subscriptionDTO.SubscriptionTypeId,
                StartDate = subscriptionDTO.StartDate
            };
            try
            {
                await _repository.UpdateCustomerSubscriptionAsync(subscription);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerSubscriptionsController/UpdateCustomerSubscription");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteCustomerSubscription/{id}")]
        public async Task<IActionResult> DeleteCustomerSubscription(int id)
        {
            try
            {
                await _repository.DeleteCustomerSubscriptionAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerSubscriptionsController/DeleteCustomerSubscription");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}