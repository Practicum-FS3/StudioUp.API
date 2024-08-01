using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo;
using StudioUp.Repo.IRepositories;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionTypeController : ControllerBase
    {
        private readonly ISubscriptionTypeRepository _repository;

        public SubscriptionTypeController(ISubscriptionTypeRepository repsitory)
        {
            _repository = repsitory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionType>>> GetSubscriptionTypes()
        {
            var subscriptionTypes = await _repository.GetAllSubscriptions();
            return Ok(subscriptionTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionTypeDTO>> GetSubscriptionType(int id)
        {
            var subscriptionType = await _repository.GetSubscriptionById(id);
            if (subscriptionType == null)
            {
                return NotFound();
            }
            return Ok(subscriptionType);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SubscriptionTypeDTO>> PutSubscriptionType(int id, SubscriptionTypeDTO subscriptionTypeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var sub = await _repository.GetSubscriptionById(id);
            if (sub == null)
                return NotFound();
            SubscriptionTypeDTO subscription = await _repository.UpdateSubscription(subscriptionTypeDto,id);
            return Ok(subscription);

        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionTypeDTO>> PostSubscriptionType(SubscriptionTypeDTO subscriptionTypeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _repository.AddSubscription(subscriptionTypeDto);
            return CreatedAtAction(nameof(GetSubscriptionType), new { id = subscriptionTypeDto.ID }, subscriptionTypeDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscriptionType(int id)
        {
            var sub= await _repository.GetSubscriptionById(id);
            if (sub == null)
                return NotFound();  
            var subToDelete=await _repository.DeleteSubscription(id);
            return Ok(subToDelete);
        }
    }
}
