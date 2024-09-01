using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudioUp.Models;
using StudioUp.DTO;
using Microsoft.EntityFrameworkCore;
using StudioUp.Repo.IRepositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;




namespace StudioUp.Repo.Repositories
{
    public class AvailableTrainingRepository : IAvailableTrainingRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ContentSectionRepository> _logger;
        private readonly ITrainingRepository _trainingRepository;
        private List<AvailableTrainingDTO> allAvailableTrainingsList;
        private List<TrainingDTO> allTrainingsList;
        public AvailableTrainingRepository(DataContext context, IMapper mapper, ILogger<ContentSectionRepository> logger,ITrainingRepository trainingRepository)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _trainingRepository = trainingRepository;
            Task.Run(() => FillListFromServer()).Wait();
        }

        // initialization lists
        private async Task FillListFromServer()
        {
            try
            {
                //Fetch Rtainings` data from server
                allTrainingsList = (await _trainingRepository.GetAllTrainings()).ToList();
                var allAvailableTrainingsList = await _context.AvailableTraining
                .Include(at => at.Training) // Include the related Training entity
                  .ToListAsync();
              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching trainings and available trainings in func FillListFromServer-Repo");
                throw;
            }
        }


        public async Task<IEnumerable<AvailableTrainingDTO>> GetAllAvailableTrainingsAsync()
        {
            try
            {
                var availableTrainings = await _context.AvailableTraining.ToListAsync();
                return _mapper.Map<IEnumerable<AvailableTrainingDTO>>(availableTrainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetAllAvailableTrainingsAsync-Repo");
                throw;
            }

        }
        public async Task<IEnumerable<CalanderAvailableTrainingDTO>> GetAllAvailableTrainingsAsyncForCalander()
        {
            var availableTrainings = await _context.AvailableTraining
                 .Include(t => t.Training)
                .Include(t => t.Training.Trainer)
                .Include(t => t.Training.TrainingCustomerType.TrainingType)
                .Include(t => t.Training.TrainingCustomerType.CustomerType)

                .ToListAsync();
            return _mapper.Map<IEnumerable<CalanderAvailableTrainingDTO>>(availableTrainings);
        }

        public async Task<AvailableTrainingDTO> GetAvailableTrainingByIdAsync(int id)
        {
            try
            {
                var availableTraining = await _context.AvailableTraining.FindAsync(id);
                return _mapper.Map<AvailableTrainingDTO>(availableTraining);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetAvailableTrainingByIdAsync-Repo");
                throw;
            }

        }

        public async Task<AvailableTrainingDTO> GetAvailableTrainingByTrainingIdAsync(int id)
        {
            var availableTraining = await _context.AvailableTraining
                .FirstOrDefaultAsync(a => a.TrainingId == id);

            if (availableTraining == null)
            {
                throw new Exception($"No available training found with TrainingId {id}");
            }

            return _mapper.Map<AvailableTrainingDTO>(availableTraining);
        }


        public async Task<AvailableTrainingDTO> AddAvailableTrainingAsync(AvailableTrainingDTO availableTrainingDTO)
        {
            try
            {
                var availableTraining = _mapper.Map<Models.AvailableTraining>(availableTrainingDTO);
                availableTraining.Id = 0;
                var newavailableTraining = await _context.AvailableTraining.AddAsync(availableTraining);

                await _context.SaveChangesAsync();
                availableTrainingDTO.Id = newavailableTraining.Entity.Id;
                return availableTrainingDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func AddAvailableTrainingAsync-Repo");
                throw;
            }
        }
        public async Task UpdateAvailableTrainingAsync(int id, AvailableTrainingDTO availableTrainingDTO)
        {
            try
            {
                var availableTraining = await _context.AvailableTraining.FindAsync(id);
                if (availableTraining == null)
                {
                    throw new Exception($"cant find availableTraining by ID {id}");
                }
                _mapper.Map(availableTrainingDTO, availableTraining);
                _context.Entry(availableTraining).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func UpdateAvailableTrainingAsync-Repo");
                throw;
            }
        }
        public async Task DeleteAvailableTrainingAsync(int id)
        {
            try
            {
                var availableTraining = await _context.AvailableTraining.FindAsync(id);
                if (availableTraining == null)
                {
                    throw new Exception($"Training with ID {id} not found");
                }
                _context.AvailableTraining.Remove(availableTraining);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func DeleteAvailableTrainingAsync-Repo");
                throw;
            }

        }

        //Generate availableTrainings for single day/week/range dates
        public async Task<bool> GenerateAvailableTrainings(DateOnly startDate, DateOnly? endDate, bool isWeekEnd)
        {
            try
            {
                //Generate date of today
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);


                // Validate input elements

                if (startDate < today)
                {
                    throw new ArgumentException("Start date and end date must be greater than or equal to today's date.");
                }
                if (endDate != null && endDate < today)
                {
                    throw new ArgumentException("Start date and end date must be greater than or equal to today's date.");
                }


                //Validate and setup Range
                //At first I assume there is no range
                int range = 0;

                if (endDate.HasValue)
                {
                    // Range contains the total number of days between startDate and endDate
                    range = (endDate.Value.DayNumber - startDate.DayNumber) + 1;


                    if (range > 30)
                    {
                        throw new ArgumentException("Range must`nt be greater than 30 days.");

                    }
                }

                // Calculate days from startDate to the next weekend on Saturday
                if (!endDate.HasValue && isWeekEnd)
                {
                    if (startDate.DayOfWeek != DayOfWeek.Saturday)
                    {
                        range = ((int)DayOfWeek.Saturday - (int)startDate.DayOfWeek + 7) % 7;

                    }

                }


                // Generate elements in indevedual function(GenerateAvailableTrainingsForDay )
                for (int i = 1; i <= range; i++)
                {
                    await GenerateAvailableTrainingsForDay(CalaulateDate(startDate, i));
                }
                // Return status

               return true;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "- this error in the func GenerateAvailableTrainings-Repo");
                return false;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GenerateAvailableTrainings-Repo");
                return false ;
            }
            
        }



       
        //Generate availableTrainings for single day
        public async Task GenerateAvailableTrainingsForDay(DateOnly targetDate)

        {

            var trainingsListInCurrentDay = allTrainingsList.FindAll(training => (DayOfWeek)training.DayOfWeek == targetDate.DayOfWeek);
            //availableTrainings that are set for the same date
            var availableTrainingsListInCurrentDay = allAvailableTrainingsList.FindAll(availableTraining => DateOnly.FromDateTime(availableTraining.Date) == targetDate);

            foreach (TrainingDTO currentTraining in trainingsListInCurrentDay)
            {
                //Specific training date and time
                DateTime currentDateTime = new DateTime(targetDate.Year, targetDate.Month, targetDate.Day, currentTraining.Hour, currentTraining.Minute, 0);

               
                // Check if there is already an AvailableTraining entry for the current date and time
                // with the same TrainingId. This ensures that the system does not create duplicate 
                // entries for the same training session at the same date and time.
                var currentAvailableTraining = allAvailableTrainingsList.FirstOrDefault(at =>
                at.TrainingId == currentTraining.ID &&
                DateOnly.FromDateTime(at.Date) == targetDate &&
                at.Date.Hour == currentDateTime.Hour &&
                at.Date.Minute == currentDateTime.Minute);


                // If there is no existing AvailableTraining for this date and time, create a new one
                if (currentAvailableTraining == null)
                {
                    //Setup and initialization of AvailableTraining 
                    AvailableTraining newAvailableTraining = new AvailableTraining
                    {
                        TrainingId = currentTraining.ID,
                        //Type of dateTimeOnly
                        Date = currentDateTime,
                        ParticipantsCount = currentTraining.ParticipantsCount,
                        IsActive = true
                    };
                    // Convert to DTO and add to the database asynchronously
                    var newAvailableTrainingDTO = _mapper.Map<AvailableTrainingDTO>(newAvailableTraining);
                    await AddAvailableTrainingAsync(newAvailableTrainingDTO);
                }
                else
                {
                    // Log that the training for this date is already defined
                    _logger.LogWarning($"LessonId: {currentTraining.ID} for {targetDate.ToString("yyyy-MM-dd")} was already defined.");

                }
            }
        }

        //cala and return targetDate=startDate+i
        public DateOnly CalaulateDate(DateOnly startDate, int i = 0)
        {
            return startDate.AddDays(i);
        }

    }
}
