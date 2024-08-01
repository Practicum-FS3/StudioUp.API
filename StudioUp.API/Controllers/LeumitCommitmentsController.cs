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
        [HttpGet("GetAll")]
        public async Task<List<LeumitCommitmentsDTO>> getAllLeumitCommitments()
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
        [HttpGet("GetById/{id}")]
        public async Task<LeumitCommitmentsDTO> getLeumitCommitmentsById(string id)
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
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<LeumitCommitmentsDTO>> update(string id, LeumitCommitmentsDTO newLeumitCommiments)
        {
           
            try
            {

                if (id != newLeumitCommiments.Id)
                    return Conflict();
                await leumitCommimentsRepository.UpdateAsync(newLeumitCommiments, id);
                return Ok(newLeumitCommiments);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> delete(string id)
        {         
            try
            {
                if (!await leumitCommimentsRepository.DeleteAsync(id))
                    return Conflict();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("Add")]
        //לבדוק את טיפוס ההחזרה
        public async Task<ActionResult<LeumitCommitmentsDTO>> add(LeumitCommitmentsDTO leumitCommimentsDTO)
        {
            try
            {
                await leumitCommimentsRepository.AddAsync(leumitCommimentsDTO);
                return CreatedAtAction(nameof(add), leumitCommimentsDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
