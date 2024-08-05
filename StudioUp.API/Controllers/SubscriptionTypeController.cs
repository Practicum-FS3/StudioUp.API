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
        private readonly IRepository<SubscriptionType> _repository;
        private readonly ILogger<SubscriptionTypeController> _logger;


        public SubscriptionTypeController(IRepository<SubscriptionType> repsitory, ILogger<SubscriptionTypeController> logger)
        {
            _repository = repsitory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionType>>> GetSubscriptionTypes()
        {
            try
            {
                var subscriptionTypes = await _repository.GetAllAsync();
                return Ok(subscriptionTypes);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, " this error in SubscriptionTypeController/GetSubscriptionTypes");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionType>> GetSubscriptionType(int id)
        {
            try
            {
                var subscriptionType = await _repository.GetByIdAsync(id);
                if (subscriptionType == null)
                {
                    return NotFound("subscriptionType not found by ID");
                }
                return Ok(subscriptionType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in SubscriptionTypeController/GetSubscriptionType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        //public async Task<ActionResult<SubscriptionTypeDTO>> PutSubscriptionType(int id, SubscriptionTypeDTO subscriptionTypeDto)
        //{
        //    if (subscriptionTypeDto == null)
        //    {
        //        return BadRequest("The subscriptionType field is null.");
        //    }
        //    if (id != subscriptionTypeDto.ID)
        //    {
        //        return BadRequest("ID in URL does not match ID in body");
        //    }
        //    try
        //    {
        //        await _repository.UpdateAsync(subscriptionTypeDto);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, " this error in SubscriptionTypeController/PutSubscriptionType");
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult<SubscriptionType>> PostSubscriptionType(SubscriptionType subscriptionType)
        {
            if (subscriptionType == null)
            {
                return BadRequest("The subscriptionType field is null.");
            }
            try
            {
               var s= await _repository.AddAsync(subscriptionType);
                return Ok(s);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in SubscriptionTypeController/PostSubscriptionType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscriptionType(int id)
        {
             try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in SubscriptionTypeController/DeleteSubscriptionType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
