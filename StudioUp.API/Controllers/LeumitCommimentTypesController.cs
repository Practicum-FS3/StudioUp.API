using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;




namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeumitCommimentTypesController: ControllerBase
    {
        private readonly ILeumitCommimentTypesRepository leumitCommimentTypesRepository;
     
        public LeumitCommimentTypesController(ILeumitCommimentTypesRepository leumitCommimentTypesRepository)
        {
            this.leumitCommimentTypesRepository = leumitCommimentTypesRepository;
          
        }
        [HttpGet("GetAll")]
        public async Task<List<LeumitCommimentTypesDTO>> getAllLeumitCommitments()
        {
            try
            {
                return await leumitCommimentTypesRepository.GetAllAsync();
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<LeumitCommimentTypesDTO>> getLeumitCommitmentsById(int id)
        {
            try
            {

                LeumitCommimentTypesDTO leumitCommimentTypesDTO = await leumitCommimentTypesRepository.GetByIdAsync(id);
                return Ok(leumitCommimentTypesDTO);
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<LeumitCommimentTypesDTO>> update(int id, LeumitCommimentTypesDTO newLeumitCommimentTypesDTO)
        {
            try
            {
            
                if (id != newLeumitCommimentTypesDTO.Id)
                    return Conflict();
                await leumitCommimentTypesRepository.UpdateAsync(newLeumitCommimentTypesDTO, id);
                return Ok(newLeumitCommimentTypesDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult>delete(int id)
        {
            try
            {
                var leumitCommimentTypes = await leumitCommimentTypesRepository.GetByIdAsync(id);
                if (leumitCommimentTypes == null)
                {
                    return NotFound($"Training with ID {id} not found.");
                }
                leumitCommimentTypes.IsActive = false;
                await leumitCommimentTypesRepository.UpdateAsync(leumitCommimentTypes, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("Add")]
     
        public async Task<ActionResult<LeumitCommimentTypesDTO>> add(LeumitCommimentTypesDTO leumitCommimentTypesDTO)
        {
            try
            {
                await leumitCommimentTypesRepository.AddAsync(leumitCommimentTypesDTO);
                return CreatedAtAction(nameof(add), leumitCommimentTypesDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
   
    }
}
