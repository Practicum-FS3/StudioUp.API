using Microsoft.EntityFrameworkCore;
using StudioUp.Repo.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.Models
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly DataContext _context;

        public TrainingRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Training>> GetAllTrainings()
        {
            return await _context.Trainings
                .Include(t => t.TrainingType)
                .Include(t => t.Trainer)
                .ToListAsync();
        }

        public async Task<Training> GetTrainingById(int id)
        {
            return await _context.Trainings
                .Include(t => t.TrainingType)
                .Include(t => t.Trainer)
                .FirstOrDefaultAsync(t => t.ID == id);
        }

        public async Task AddTraining(Training training)
        {
            _context.Trainings.Add(training);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTraining(Training training)
        {
            _context.Trainings.Update(training);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTraining(int id)
        {
            var training = await _context.Trainings.FindAsync(id);
            if (training != null)
            {
                _context.Trainings.Remove(training);
                await _context.SaveChangesAsync();
            }
        }
    }
}
