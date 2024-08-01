using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HMOController : ControllerBase
    {
        readonly IHMORepository HMOService;

        public HMOController(IHMORepository HMOService)
        {
            this.HMOService = HMOService;
        }

        [HttpGet("GetAll")]
        public async Task<List<DTO.HMODTO>> GetAll()
        {
            try
            {
                return await HMOService.GetAllAsync();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("Add")]
        public async Task<HMODTO> add(DTO.HMODTO hmo)
        {
            try
            {
                return await HMOService.AddAsync(hmo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                var HMO = await HMOService.GetByIdAsync(id);
                if (HMO == null)
                {
                    return NotFound($"Training with ID {id} not found.");
                }

                HMO.IsActive = false;
                await HMOService.UpdateAsync(id, HMO);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<DTO.HMODTO> getById(int id)
        {
            try
            {
                return await HMOService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<bool> update(int id , DTO.HMODTO hmo)
        {
            try
            {
                return await HMOService.UpdateAsync(id ,hmo);
            }
            catch(Exception ex) {
                throw ex;
            }

        }

   }
}
