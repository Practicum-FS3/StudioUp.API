//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using StudioUp.DTO;
//using StudioUp.Models;
//using StudioUp.Repo.IRepositories;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace StudioUp.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CustomerSubscriptionsController : ControllerBase
//    {
//        private readonly ICustomerSubscriptionRepository _repository;

//        public CustomerSubscriptionsController(ICustomerSubscriptionRepository repository)
//        {
//            _repository = repository;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<CustomerSubscriptionDTO>>> GetAllCustomerSubscriptions()
//        {
//            var subscriptions = await _repository.GetAllCustomerSubscriptionsAsync();
//            // המרה ל-DTO או הוספת אוטומאפר במקרה שזה קיים
//            return Ok(subscriptions);
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<CustomerSubscriptionDTO>> GetCustomerSubscriptionById(int id)
//        {
//            var subscription = await _repository.GetCustomerSubscriptionByIdAsync(id);
//            if (subscription == null)
//            {
//                return NotFound();
//            }
//            // המרה ל-DTO במקרה הצורך
//            return Ok(subscription);
//        }

//        [HttpPost]
//        public async Task<ActionResult> AddCustomerSubscription(CustomerSubscriptionDTO subscriptionDTO)
//        {
//            // המרת DTO לאובייקט המודל
//            var subscription = new CustomerSubscription
//            {
//                CustomerID = subscriptionDTO.CustomerID,
//                SubscriptionTypeId = subscriptionDTO.SubscriptionTypeId,
//                StartDate = subscriptionDTO.StartDate
//            };

//            await _repository.AddCustomerSubscriptionAsync(subscription);
//            return CreatedAtAction(nameof(GetCustomerSubscriptionById), new { id = subscription.ID }, subscription);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateCustomerSubscription(int id, CustomerSubscriptionDTO subscriptionDTO)
//        {
//            if (id != subscriptionDTO.ID)
//            {
//                return BadRequest();
//            }

//            var subscription = new CustomerSubscription
//            {
//                ID = subscriptionDTO.ID,
//                CustomerID = subscriptionDTO.CustomerID,
//                SubscriptionTypeId = subscriptionDTO.SubscriptionTypeId,
//                StartDate = subscriptionDTO.StartDate
//            };

//            try
//            {
//                await _repository.UpdateCustomerSubscriptionAsync(subscription);
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                return NotFound();
//            }

//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteCustomerSubscription(int id)
//        {
//            await _repository.DeleteCustomerSubscriptionAsync(id);
//            return NoContent();
//        }
//    }
//}
