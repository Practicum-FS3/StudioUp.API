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


        public TrainingRepository(DataContext context, IMapper mapper, ILogger<TrainingRepository> logger)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainingDTO>> GetAllTrainingsAsync()
        {
            try
            {
                List<Training> lst = await _context.Trainings
                              .Include(t => t.TrainingCustomerType)
                              .Include(t => t.Trainer)
                              .ToListAsync();
                return _mapper.Map<IEnumerable<TrainingDTO>>(lst);
            }
            catch 
            {
                throw;
            }

        }


        public async Task<IEnumerable<CalanderTrainingDTO>> GetAllTrainingsCalenderAsync()
        {

            try
            {
               List<Training> lst = await _context.Trainings
                .Include(t => t.TrainingCustomerType.CustomerType)
                .Include(t => t.TrainingCustomerType.TrainingType)
                .Include(t => t.Trainer)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CalanderTrainingDTO>>(lst);
            }
            catch
            {
                throw;
            }
        }

        public async Task<TrainingDTO> GetTrainingByIdAsync(int id)
        {
            try
            {
                Training training = await _context.Trainings.Include(t => t.TrainingCustomerType).Include(t => t.Trainer).FirstOrDefaultAsync(t => t.ID == id);
                return _mapper.Map<TrainingDTO>(training);
            }
            catch
            {
                throw;
            }

        }

        public async Task<TrainingDTO> AddTrainingAsync(TrainingDTO trainingDto)
        {
            try
            {
                var x = await _context.Trainings.AddAsync(_mapper.Map<Training>(trainingDto));
                await _context.SaveChangesAsync();
                return trainingDto;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<TrainingDTO> UpdateTrainingAsync(TrainingDTO trainingDto, int id)
        {
            try
            {
                Training training = await _context.Trainings.FirstOrDefaultAsync(t => t.ID == id);
                _mapper.Map(trainingDto, training);
                _context.Trainings.Update(training);
                await _context.SaveChangesAsync();
                return trainingDto;
            }
            catch 
            {
                throw;
            }

        }

        public async Task DeleteTrainingAsync(int id)
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
            catch 
            {
                throw;
            }

        }

       
    }
}
