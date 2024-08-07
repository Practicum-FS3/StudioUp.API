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




namespace StudioUp.Repo.Repositories
{
    public class AvailableTrainingRepository : IAvailableTrainingRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AvailableTrainingRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AvailableTrainingDTO>> GetAllAvailableTrainingsAsync()
        {
            var availableTrainings = await _context.AvailableTraining.ToListAsync();
            return _mapper.Map<IEnumerable<AvailableTrainingDTO>>(availableTrainings);
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
            var availableTraining = await _context.AvailableTraining.FindAsync(id);
            return _mapper.Map<AvailableTrainingDTO>(availableTraining);
        }

        public async Task<CalanderAvailableTrainingDTO> GetAvailableByTrainingIdForCalander(int id)
        {
            var availableTraining = await _context.AvailableTraining
                                 .Include(t => t.Training)
                .Include(t => t.Training.Trainer)
                .Include(t => t.Training.TrainingCustomerType.TrainingType)
                .Include(t => t.Training.TrainingCustomerType.CustomerType)
                .FirstOrDefaultAsync(a => a.TrainingId == id);
            return _mapper.Map<CalanderAvailableTrainingDTO>(availableTraining);
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
            var availableTraining = _mapper.Map<Models.AvailableTraining>(availableTrainingDTO);
            availableTraining.Id = 0;
            var newavailableTraining = await _context.AvailableTraining.AddAsync(availableTraining);
 
            await _context.SaveChangesAsync();
            availableTrainingDTO.Id = newavailableTraining.Entity.Id;
            return availableTrainingDTO;
        }
        public async Task<AvailableTrainingDTO> UpdateAvailableTrainingAsync(int id, AvailableTrainingDTO availableTrainingDTO)
        {
            var availableTraining = await _context.AvailableTraining.FindAsync(id);
            if (availableTraining == null) return null;

            _mapper.Map(availableTrainingDTO, availableTraining);
            _context.Entry(availableTraining).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _mapper.Map<AvailableTrainingDTO>(availableTraining);
        }
        public async Task<bool> DeleteAvailableTrainingAsync(int id)
        {
            var availableTraining = await _context.AvailableTraining.FindAsync(id);
            if (availableTraining == null) return false;

            _context.AvailableTraining.Remove(availableTraining);
            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}
