﻿using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HMOControllers : ControllerBase
    {
        readonly IHMORepository HMOService;

        public HMOControllers(IHMORepository HMOService)
        {
            this.HMOService = HMOService;
        }

        [HttpGet("GetAll")]
        [Route ("/getAll")]
        public async Task<List<DTO.HMODTO>> GetAll()
        {
            try
            {
                return await HMOService.GetAllAsync();
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("Add")]
        [Route ("/add")]
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
        [Route("/delete/{id}")]
        public async Task<bool> delete(int id)
        {
            try
            {
                return await HMOService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetById/{id}")]
        [Route ("/getById/{id}")]
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
        [Route ("/update")]
        public async Task<bool> update(DTO.HMODTO hmo)
        {
            try
            {
                return await HMOService.UpdateAsync(hmo);
            }
            catch(Exception ex) {
                throw ex;
            }

        }

   }
}
