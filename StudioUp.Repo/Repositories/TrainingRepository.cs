using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TrainingRepository> _logger;


        public TrainingRepository(DataContext context, IMapper mapper, ILogger<TrainingRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<TrainingDTO>> GetAllTrainings()
        {
            try
            {
                List<Training> lst = await _context.Trainings
                              .Include(t => t.TrainingCustomerType)
                              .Include(t => t.Trainer)
                              .ToListAsync();
                return _mapper.Map<IEnumerable<TrainingDTO>>(lst);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetAllTrainings-Repo");
                throw;
            }

        }


        public async Task<IEnumerable<CalanderTrainingDTO>> GetAllTrainingsCalender()
        {
            try
            {
                List<Training> lst = await _context.Trainings
                                //.Include(t => t.Trainer.FirstName + " " + t.Trainer.LastName)
                                .Include(t => t.Trainer)
                                .ToListAsync();
                return _mapper.Map<IEnumerable<CalanderTrainingDTO>>(lst);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetAllTrainingsCalender-Repo");
                throw;
            }

        }

        public async Task<TrainingDTO> GetTrainingById(int id)
        {
            try
            {
                Training training = await _context.Trainings
                               .Include(t => t.TrainingCustomerType)
                               .Include(t => t.Trainer)
                               .FirstOrDefaultAsync(t => t.ID == id);
                return _mapper.Map<TrainingDTO>(training);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetTrainingById-Repo");
                throw;
            }

        }

        public async Task<TrainingDTO> AddTraining(TrainingDTO trainingDto)
        {
            try
            {
                Training training = _mapper.Map<Training>(trainingDto);
                _context.Trainings.Add(training);
                await _context.SaveChangesAsync();
                return trainingDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func AddTraining-Repo");
                throw;
            }

        }

        public async Task UpdateTraining(TrainingDTO trainingDto, int id)
        {
            try
            {
                Training training = await _context.Trainings.FirstOrDefaultAsync(t => t.ID == id);
                _mapper.Map(trainingDto, training);
                _context.Trainings.Update(training);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func UpdateTraining-Repo");
                throw;
            }

        }

        public async Task DeleteTraining(int id)
        {
            try
            {
                var training = await _context.Trainings.FindAsync(id);
                if (training == null)
                {

                }
                _context.Trainings.Remove(training);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func UpdateTraining-Repo");
                throw;
            }

        }
    }
}
