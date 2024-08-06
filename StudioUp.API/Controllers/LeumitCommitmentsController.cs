// LeumitCommitmentsController
using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Repo.IRepositories;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeumitCommitmentsController : ControllerBase
    {
        private readonly ILeumitCommimentsRepository leumitCommimentsRepository;

        public LeumitCommitmentsController(ILeumitCommimentsRepository leumitCommimentsRepository)
        {
            this.leumitCommimentsRepository = leumitCommimentsRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<LeumitCommitmentsDTO>>> GetAll()
        {
            try
            {
                var commitments = await leumitCommimentsRepository.GetAllAsync();
                return Ok(commitments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<LeumitCommitmentsDTO>> GetById(string id)
        {
            try
            {
                var commitment = await leumitCommimentsRepository.GetByIdAsync(id);
                if (commitment == null)
                {
                    return NotFound($"LeumitCommitments with ID {id} not found.");
                }
                return Ok(commitment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string id, LeumitCommitmentsDTO leumitCommitmentsDTO)
        {
            if (leumitCommitmentsDTO == null)
            {
                return BadRequest("The leumitCommitmentsDTO field is null.");
            }

            if (id != leumitCommitmentsDTO.Id)
            {
                return Conflict("ID in the path does not match ID in the body.");
            }

            try
            {
                var updatedCommitment = await leumitCommimentsRepository.UpdateAsync(leumitCommitmentsDTO, id);
                if (updatedCommitment == null)
                {
                    return NotFound("Commitment not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var isDeleted = await leumitCommimentsRepository.DeleteAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"LeumitCommitments with ID {id} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<LeumitCommitmentsDTO>> Add(LeumitCommitmentsDTO leumitCommitmentsDTO)
        {
            if (leumitCommitmentsDTO == null)
            {
                return BadRequest("The leumitCommitmentsDTO field is null.");
            }

            try
            {
                await leumitCommimentsRepository.AddAsync(leumitCommitmentsDTO);
                return CreatedAtAction(nameof(Add), leumitCommitmentsDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
