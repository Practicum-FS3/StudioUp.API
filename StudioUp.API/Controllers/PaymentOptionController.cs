using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Repo;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentOptionController : ControllerBase
    {
        private readonly IRepository<PaymentOptionDTO> _repository;
        private readonly ILogger<PaymentOptionController> _logger;


        public PaymentOptionController(IRepository<PaymentOptionDTO> repsitory, ILogger<PaymentOptionController> logger)
        {
            _repository = repsitory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentOptionDTO>>> GetPaymentOptions()
        {
            try
            {
                var paymentOptions = await _repository.GetAllAsync();
                return Ok(paymentOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in PaymentOptionController/GetPaymentOptions");
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentOptionDTO>> GetPaymentOption(int id)
        {
            try
            {
                var paymentOption = await _repository.GetByIdAsync(id);
                if (paymentOption == null)
                {
                    return NotFound("payment option not found by ID");
                }
                return Ok(paymentOption);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in PaymentOptionController/GetPaymentOption");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentOption(int id, PaymentOptionDTO paymentOption)
        {
            if (paymentOption == null)
            {
                return BadRequest("The payment option field is null.");
            }
            if (id != paymentOption.ID)
            {
                return BadRequest("ID in URL does not match ID in body");
            }
            try
            {
                await _repository.UpdateAsync(paymentOption);
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, " this error in PaymentOptionController/PutPaymentOption");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost]
        public async Task<ActionResult<PaymentOptionDTO>> PostPaymentOption(PaymentOptionDTO paymentOption)
        {
            if (paymentOption == null)
            {
                return BadRequest("The payment option field is null.");
            }
            try
            {
                var p = await _repository.AddAsync(paymentOption);
                return Ok(p);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in PaymentOptionController/PostPaymentOption");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentOption(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in PaymentOptionController/DeletePaymentOption");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
