using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog.Filters;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailableTrainingController : ControllerBase
    {
        private readonly IAvailableTrainingRepository _availableTrainingRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly ILogger<AvailableTrainingController> _logger;

        public AvailableTrainingController(IAvailableTrainingRepository availableTrainingRepository,ITrainingRepository trainingRepository, ILogger<AvailableTrainingController> logger)
        {
            _availableTrainingRepository = availableTrainingRepository;
            _trainingRepository= trainingRepository;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<AvailableTrainingDTO>>> GetAllAvailableTrainings()
        {
            try
            {
                var availableTrainingsDTO = await _availableTrainingRepository.GetAllAvailableTrainingsAsync();
                return Ok(availableTrainingsDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in AvailableTrainingController/GetAllAvailableTrainings");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/AvailableTraining/forCalander

        [HttpGet("forCalander")]
        public async Task<ActionResult<IEnumerable<AvailableTrainingDTO>>> GetAvailableTrainingsCalender()
        {
            var availableTrainingsDTO = await _availableTrainingRepository.GetAllAvailableTrainingsAsyncForCalander();
            return Ok(availableTrainingsDTO);
        }

        //[HttpGet("{id}")]
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<AvailableTrainingDTO>> GetByIdAvailableTraining(int id)
        {
            try
            {
                var availableTrainingDTO = await _availableTrainingRepository.GetAvailableTrainingByIdAsync(id);
                if (availableTrainingDTO == null)
                {
                    return NotFound("available training not found by ID");
                }
                return Ok(availableTrainingDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in AvailableTrainingController/GetByIdAvailableTraining");
                return StatusCode(500, $"Internal server error: {ex.Message} ");
            }
        }
        [HttpPost("Add")]
        public async Task<ActionResult<AvailableTrainingDTO>> CreateAvailableTraining([FromBody] AvailableTrainingDTO availableTrainingDTO)
        {
            if (availableTrainingDTO == null)
            {
                return BadRequest("The availableTrainingDTO field is null.");
            }
            try
            {
                await _availableTrainingRepository.AddAvailableTrainingAsync(availableTrainingDTO);
                return CreatedAtAction(nameof(GetByIdAvailableTraining), new { id = availableTrainingDTO.Id }, availableTrainingDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in AvailableTrainingController/CreateAvailableTraining");
                return StatusCode(500, $"Internal server error: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAvailableTraining(int id, [FromBody] AvailableTrainingDTO availableTrainingDTO)
        {
            if (availableTrainingDTO == null)
            {
                return BadRequest("AvailableTrainingDTO object is null");
            }
            if (id != availableTrainingDTO.Id)
            {
                return BadRequest("ID in URL does not match ID in body");
            }
            try
            {
                await _availableTrainingRepository.UpdateAvailableTrainingAsync(id, availableTrainingDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in AvailableTrainingController/UpdateAvailableTrainingAsync");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAvailableTraining(int id)
        {
            try
            {
                await _availableTrainingRepository.DeleteAvailableTrainingAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in AvailableTrainingController/DeleteAvailableTraining");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }








        ////Generate availableTrainings for single day/week/range dates
        //public async Task<ActionResult<IEnumerable<bool>>> GenerateAvailableTrainings(DateOnly startDate, DateOnly? endDate, bool isWeekEnd)
        //{
        //    try
        //    {
        //        //Generate date of today
        //        DateOnly today = DateOnly.FromDateTime(DateTime.Now);


        //        // Validate input elements

        //        if (startDate < today)
        //        {
        //            throw new ArgumentException("Start date and end date must be greater than or equal to today's date.");
        //        }
        //        if (endDate != null && endDate < today)
        //        {
        //            throw new ArgumentException("Start date and end date must be greater than or equal to today's date.");
        //        }


        //        //Validate and setup Range
        //        //At first I assume there is no range
        //        int range = 0;

        //        if (endDate.HasValue)
        //        {
        //            // Range contains the total number of days between startDate and endDate
        //            range = (endDate.Value.ToDateTime(TimeOnly.MinValue) - startDate.ToDateTime(TimeOnly.MinValue)).Days;

        //            if (range >30)
        //            {
        //                throw new ArgumentException("Range must`nt be greater than 30 days.");

        //            }
        //        }

        //        // Calculate days from startDate to the next weekend on Saturday
        //        if (!endDate.HasValue && isWeekEnd)
        //        {
        //            if (startDate.DayOfWeek != DayOfWeek.Saturday)
        //            {
        //              range= (DayOfWeek.Saturday - startDate.DayOfWeek + 7) % 7;
        //            }
                    
        //        }


        //        // Generate elements in indevedual function(GenerateAvailableTrainingsForDay )
        //        for (int i = 1; i <= range; i++)
        //        {
        //            GenerateAvailableTrainingsForDay( CalaulateDate(startDate, range));
        //        }
        //        // Return status

        //        return Ok(true);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        _logger.LogError(ex, "Invalid input parameters in AvailableTrainingController/GenerateAvailableTrainings");
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "This error occurred in AvailableTrainingController/GenerateAvailableTrainings");
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        ////Generate availableTrainings for single day
        //public async void GenerateAvailableTrainingsForDay(DateOnly targetDate)
        //{
        //    //Fetch Rtainings` data from server
        //    IEnumerable<TrainingDTO> allTrainingsList = await _trainingRepository.GetAllTrainings();
        //    IEnumerable<AvailableTrainingDTO> allAvailableTrainingsList = await _availableTrainingRepository.GetAllAvailableTrainingsAsync();

        //    //Filtering the trainings of that targetDate/day
        //    List<TrainingDTO> trainingsListInCurrentDay = allTrainingsList.ToList().FindAll(trainer => trainer.DayOfWeek == targetDate.Day);
        //    List<AvailableTrainingDTO> availableTrainingsListInCurrentDay = allAvailableTrainingsList.ToList().FindAll(availableTraining => availableTraining.Date == targetDate);

        //  //  Check if its a correct rule
        //    if (trainingsListInCurrentDay.Count == availableTrainingsListInCurrentDay.Count)
        //    {
        //        // Logging a message with a variable
        //        _logger.LogWarning($"Lessons for {targetDate.ToString("yyyy-MM-dd")} were already defined.");
        //        return;
        //    }

        //    foreach (TrainingDTO currentTraining in trainingsListInCurrentDay)
        //    {
        //        //Validate searching for exist AvailableTraining in current date
        //        AvailableTrainingDTO currentAvailableTraining = availableTrainingsListInCurrentDay.FirstOrDefault(availableTraining => availableTraining.TrainingId == currentTraining.ID);

        //        //AvailableTraining for currentDate wasnt realloc yet
        //        if (currentAvailableTraining == null)
        //        {
        //            ////Setup and initialization of AvailableTraining 2 options

        //            ///1)    AvailableTrainingDTO option


        //            //currentAvailableTraining = new AvailableTrainingDTO
        //            //{
        //            //    TrainingId = currentTraining.ID,
        //            //    Date = targetDate,
        //            //    ParticipantsCount = currentTraining.ParticipantsCount,
        //            //    IsActive = true
        //            //};

        //            //   await _availableTrainingRepository.AddAvailableTrainingAsync(currentAvailableTraining);

        //            ///2)      AvailableTraining option

        //            //AvailableTraining newAvailableTraining = new AvailableTraining
        //            //{
        //            //    TrainingId = currentTraining.ID,
        //            //    Date = targetDate,
        //            //    ParticipantsCount = currentTraining.ParticipantsCount,
        //            //    IsActive = true
        //            //};
        //            // await _availableTrainingRepository.AddAvailableTrainingAsync(newAvailableTraining);
        //        }
        //        else {
        //            //??
        //            //Write to log this AvailableTraining exist in system
        //            //or nothing?

        //        }
        //    }
        //}

        ////cala and return targetDate=startDate+i
        //public DateOnly CalaulateDate(DateOnly startDate, int i = 0)
        //{
        //  return startDate.AddDays(i);
        //}

    }
}
