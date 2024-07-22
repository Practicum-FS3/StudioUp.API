using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioUp.Models;
using StudioUp.Repo;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentOptionController : ControllerBase
    {
        private readonly IRepository<PaymentOption> _repository;

        public PaymentOptionController(IRepository<PaymentOption> repsitory)
        {
            _repository = repsitory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentOption>>> GetPaymentOptions()
        {
            var paymentOptions = await _repository.GetAllAsync();
            return Ok(paymentOptions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPaymentOption(int id)
        {
            var paymentOption = await _repository.GetByIdAsync(id);
            if (paymentOption == null)
            {
                return NotFound();
            }
            return Ok(paymentOption);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentOption(int id, PaymentOption paymentOption)
        {
            if (id != paymentOption.ID)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(paymentOption);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<object>> PostPaymentOption(PaymentOption paymentOption)
        {
            await _repository.AddAsync(paymentOption);
            return CreatedAtAction(nameof(GetPaymentOption), new { id = paymentOption.ID }, paymentOption);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentOption(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
