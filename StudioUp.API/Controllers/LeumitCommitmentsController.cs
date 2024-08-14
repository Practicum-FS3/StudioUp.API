using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;
namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeumitCommitmentsController: ControllerBase
    {
        private readonly ILeumitCommimentsRepository leumitCommimentsRepository;

        public LeumitCommitmentsController(ILeumitCommimentsRepository leumitCommimentsRepository)
        {
            this.leumitCommimentsRepository = leumitCommimentsRepository;
        }
        [HttpGet("GetAllLeumitCommitments")]
        public async Task<List<LeumitCommitmentsDTO>> GetAllLeumitCommitments()
        {
            try
            {
                return await leumitCommimentsRepository.GetAllAsync();
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        [HttpGet("GetLeumitCommitmentById/{id}")]
        public async Task<LeumitCommitmentsDTO> GetLeumitCommitmentById(string id)
        {
            try
            {
                return await leumitCommimentsRepository.GetByIdAsync(id);
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        [HttpPut("UpdateLeumitCommitment")]
        public async Task<ActionResult<LeumitCommitmentsDTO>> UpdateLeumitCommitment( LeumitCommitmentsDTO newLeumitCommiments)
        {
           
            try
            {
                await leumitCommimentsRepository.UpdateAsync(newLeumitCommiments);
                return Ok(newLeumitCommiments);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete("DeleteLeumitCommitment/{id}")]
        public async Task<ActionResult> DeleteLeumitCommitment(string id)
        {         
            try
            {
                var leumitCommiment = await leumitCommimentsRepository.GetByIdAsync(id);
                if (leumitCommiment == null)
                {
                    return NotFound($"Training with ID {id} not found.");
                }
                leumitCommiment.IsActive = false;
                await leumitCommimentsRepository.UpdateAsync(leumitCommiment);
                return NoContent();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("AddLeumitCommitment")]
        //לבדוק את טיפוס ההחזרה
        public async Task<ActionResult<LeumitCommitmentsDTO>> AddLeumitCommitment(LeumitCommitmentsDTO leumitCommimentsDTO)
        {
            try
            {
                await leumitCommimentsRepository.AddAsync(leumitCommimentsDTO);
                return CreatedAtAction(nameof(AddLeumitCommitment), leumitCommimentsDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
