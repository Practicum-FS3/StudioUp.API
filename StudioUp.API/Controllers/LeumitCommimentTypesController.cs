﻿using AutoMapper;
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
        [HttpGet("GetAllLeumitCommitments")]
        public async Task<List<LeumitCommimentTypesDTO>> GetAllLeumitCommitments()
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
        [HttpGet("GetLeumitCommitmentTypesById/{id}")]
        public async Task<ActionResult<LeumitCommimentTypesDTO>> GetLeumitCommitmentTypesById(int id)
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
        [HttpPut("UpdateLeumitCommitmentType")]
        public async Task<ActionResult<LeumitCommimentTypesDTO>> UpdateLeumitCommitmentType( LeumitCommimentTypesDTO newLeumitCommimentTypesDTO)
        {
            try
            {
                await leumitCommimentTypesRepository.UpdateAsync(newLeumitCommimentTypesDTO);
                return Ok(newLeumitCommimentTypesDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpDelete("DeleteLeumitCommitmentType/{id}")]
        public async Task<ActionResult> DeleteLeumitCommitmentType(int id)
        {
            try
            {
                var leumitCommimentTypes = await leumitCommimentTypesRepository.GetByIdAsync(id);
                if (leumitCommimentTypes == null)
                {
                    return NotFound($"Training with ID {id} not found.");
                }
                leumitCommimentTypes.IsActive = false;
                await leumitCommimentTypesRepository.UpdateAsync(leumitCommimentTypes);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("AddLeumitCommitmentType")]
     
        public async Task<ActionResult<LeumitCommimentTypesDTO>> AddLeumitCommitmentType(LeumitCommimentTypesDTO leumitCommimentTypesDTO)
        {
            try
            {
                await leumitCommimentTypesRepository.AddAsync(leumitCommimentTypesDTO);
                return CreatedAtAction(nameof(AddLeumitCommitmentType), leumitCommimentTypesDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
   
    }
}
