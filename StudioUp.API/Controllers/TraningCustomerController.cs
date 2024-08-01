using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraningCustomerController : ControllerBase
    {
        private readonly ITrainingCustomerRepository _trainingCustomerRepository;

        public TraningCustomerController(ITrainingCustomerRepository trainingCustomerRepository)
        {
            _trainingCustomerRepository = trainingCustomerRepository;
        }

        // GET: api/TraningCustomer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingCustomerDTO>>> Get()
        {
            var trainingsCustomer = await _trainingCustomerRepository.GetAllTrainingCustomers();
            return Ok(trainingsCustomer);
        }

        // GET api/TraningCustomer/byId/5
        [HttpGet("byId/{id}")]
        public async Task<ActionResult<TrainingCustomerDTO>> Get(int id)
        {
            var trainingCustomer = await _trainingCustomerRepository.GetTraningCustomerById(id);
            if (trainingCustomer == null)
            {
                return NotFound();
            }
            return Ok(trainingCustomer);
        }

        // GET api/TraningCustomer/byTrainingId/5
        [HttpGet("byTrainingId/{id}")]
        public async Task<ActionResult<IEnumerable<TrainingCustomerDTO>>> GetByTraning(int id)
        {
            var trainingsCustomer = await _trainingCustomerRepository.GetTraningCustomerByTraningId(id);
            if (trainingsCustomer == null || !trainingsCustomer.Any())
            {
                return NotFound();
            }
            return Ok(trainingsCustomer);
        }

        // GET api/TraningCustomer/byCustomerId/5
        [HttpGet("byCustomerId/{id}")]
        public async Task<ActionResult<IEnumerable<TrainingCustomerDTO>>> GetByCustomer(int id)
        {
            var trainingsCustomer = await _trainingCustomerRepository.GetTraningCustomerByCustomerId(id);
            if (trainingsCustomer == null || !trainingsCustomer.Any())
            {
                return NotFound();
            }
            return Ok(trainingsCustomer);
        }

        // POST api/TraningCustomer/AddTrainingCustomer
        [HttpPost("AddTrainingCustomer")]
        public async Task<ActionResult<TrainingCustomerDTO>> Post(TrainingCustomerDTO trainingCustomer)
        {
            try
            {
                var newTrainingCustomer = await _trainingCustomerRepository.AddTraningCustomer(trainingCustomer);
                return CreatedAtAction(nameof(Get), new { id = newTrainingCustomer.ID }, newTrainingCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT api/TraningCustomer/UpdateTrainingCustomer/5
        [HttpPut("UpdateTrainingCustomer/{id}")]
        public async Task<IActionResult> Put(int id, TrainingCustomerDTO trainingCustomer)
        {
            try
            {
                var existingTrainingCustomer = await _trainingCustomerRepository.GetTraningCustomerById(id);
                if (existingTrainingCustomer == null)
                {
                    return NotFound();
                }

                trainingCustomer.ID = id;
                var result = await _trainingCustomerRepository.UpdateTrainingCustomers(trainingCustomer);
                if (result)
                {
                    return NoContent();
                }

                return StatusCode(500, "Error updating the customer");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE api/TraningCustomer/DeleteTrainingCustomer/5
        [HttpDelete("DeleteTrainingCustomer/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var trainingCustomer = await _trainingCustomerRepository.GetTraningCustomerById(id);
                if (trainingCustomer == null)
                {
                    return NotFound($"Training with ID {id} not found.");
                }

                trainingCustomer.IsActive = false;
                await _trainingCustomerRepository.UpdateTrainingCustomers(trainingCustomer);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
