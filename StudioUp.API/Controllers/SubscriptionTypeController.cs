using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioUp.Models;
using StudioUp.Repo;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionTypeController : ControllerBase
    {
        private readonly IRepository<SubscriptionType> _repository;

        public SubscriptionTypeController(IRepository<SubscriptionType> repsitory)
        {
            _repository = repsitory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionType>>> GetSubscriptionTypes()
        {
            var subscriptionTypes = await _repository.GetAllAsync();
            return Ok(subscriptionTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetSubscriptionType(int id)
        {
            var subscriptionType = await _repository.GetByIdAsync(id);
            if (subscriptionType == null)
            {
                return NotFound();
            }
            return Ok(subscriptionType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscriptionType(int id, SubscriptionType subscriptionType)
        {
            if (id != subscriptionType.ID)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(subscriptionType);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<object>> PostSubscriptionType(SubscriptionType subscriptionType)
        {
            await _repository.AddAsync(subscriptionType);
            return CreatedAtAction(nameof(GetSubscriptionType), new { id = subscriptionType.ID }, subscriptionType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscriptionType(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
