using StudioUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace StudioUp.Repo.Repositories
{
    public class TrainigTypeRepository : IRepository<TrainingType>
    {



        private readonly DataContext _context;

        public TrainigTypeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<TrainingType>> GetAllAsync()
        {
            return await _context.TrainingTypes.ToListAsync();
        }

        public async Task<TrainingType> GetByIdAsync(int id)
        {
            return await _context.TrainingTypes.FindAsync(id);
        }

        public async Task<TrainingType> AddAsync(TrainingType TrainingType)
        {
            await _context.TrainingTypes.AddAsync(TrainingType);
            await _context.SaveChangesAsync();
            return TrainingType;
        }

        public async Task UpdateAsync(TrainingType TrainingType)
        {
            _context.TrainingTypes.Update(TrainingType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var TrainingType = await _context.TrainingTypes.FindAsync(id);
            if (TrainingType != null)
            {
                _context.TrainingTypes.Remove(TrainingType);
                await _context.SaveChangesAsync();
            }
        }
    }
}

