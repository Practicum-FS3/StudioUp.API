using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudioUp.Models;
using StudioUp.DTO;
using Microsoft.EntityFrameworkCore;
using StudioUp.Repo.IRepositories;




namespace StudioUp.Repo.Repositories
{
    public class AvailableTrainingRepository : IAvailableTrainingRepository
    {
        private readonly DataContext _context;

        public AvailableTrainingRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AvailableTrainingDTO>> GetAllAvailableTrainingsAsync()
        {
            var availableTrainings = await _context.AvailableTraining.ToListAsync();
            return availableTrainings.Select(at => new AvailableTrainingDTO
            {
                Id = at.Id,
                TrainingId = at.TrainingId,
                Date = at.Date,
                ParticipantsCount = at.ParticipantsCount
            }).ToList();
        }

        public async Task<AvailableTrainingDTO> GetAvailableTrainingByIdAsync(int id)
        {
            var availableTraining = await _context.AvailableTraining.FindAsync(id);
            if (availableTraining == null)
            {
                return null;
            }

            return new AvailableTrainingDTO
            {
                Id = availableTraining.Id,
                TrainingId = availableTraining.TrainingId,
                Date = availableTraining.Date,
                ParticipantsCount = availableTraining.ParticipantsCount
            };
        }
        public async Task AddAvailableTrainingAsync(AvailableTrainingDTO availableTrainingDTO)
        {
            var availableTraining = new AvailableTraining
            {
                TrainingId = availableTrainingDTO.TrainingId,
                Date = availableTrainingDTO.Date,
                ParticipantsCount = availableTrainingDTO.ParticipantsCount,
                //IsActive = true // Assuming new trainings are active by default
            };

            await _context.AvailableTraining.AddAsync(availableTraining);
            await _context.SaveChangesAsync();

            availableTrainingDTO.Id = availableTraining.Id;
        }
        public async Task UpdateAvailableTrainingAsync(AvailableTrainingDTO availableTrainingDTO)
        {
            var existingAvailableTraining = await _context.AvailableTraining.FindAsync(availableTrainingDTO.Id);
            if (existingAvailableTraining != null)
            {
                existingAvailableTraining.TrainingId = availableTrainingDTO.TrainingId;
                existingAvailableTraining.Date = availableTrainingDTO.Date;
                existingAvailableTraining.ParticipantsCount = availableTrainingDTO.ParticipantsCount;

                _context.AvailableTraining.Update(existingAvailableTraining);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAvailableTrainingAsync(int id)
        {
            var availableTraining = await _context.AvailableTraining.FindAsync(id);
            if (availableTraining != null)
            {
                _context.AvailableTraining.Remove(availableTraining);
                await _context.SaveChangesAsync();
            }
        }
    }
}
