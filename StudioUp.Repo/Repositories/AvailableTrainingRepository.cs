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




namespace StudioUp.Repo.Repositories
{
    public class AvailableTrainingRepository : IAvailableTrainingRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ContentSectionRepository> _logger;

        public AvailableTrainingRepository(DataContext context, IMapper mapper, ILogger<ContentSectionRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
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


    }
}
