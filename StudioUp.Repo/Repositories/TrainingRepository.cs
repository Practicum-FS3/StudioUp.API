﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TrainingRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainingDTO>> GetAllTrainings()
        {
            List<Training> lst = await _context.Trainings
                //.Include(t => t.TrainingCustomerType)
                .Include(t => t.Trainer)
                .ToListAsync();
            return _mapper.Map<IEnumerable<TrainingDTO>>(lst);
        }


        public async Task<IEnumerable<CalanderTrainingDTO>> GetAllTrainingsCalender()
        {
            List<Training> lst = await _context.Trainings
                //.Include(t => t.Trainer.FirstName + " " + t.Trainer.LastName)
                .Include(t => t.Trainer)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CalanderTrainingDTO>>(lst);
        }

        public async Task<TrainingDTO> GetTrainingById(int id)
        {
            Training training= await _context.Trainings
                //.Include(t => t.TrainingCustomerType)
                .Include(t => t.Trainer)
                .FirstOrDefaultAsync(t => t.ID == id);
            return _mapper.Map<TrainingDTO>(training);
        }

        public async Task AddTraining(TrainingDTO trainingDto)
        {
            Training training=_mapper.Map<Training>(trainingDto);
            _context.Trainings.Add(training);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTraining(TrainingDTO trainingDto, int id)
        {
            Training training=_context.Trainings.FirstOrDefault(t => t.ID == id);
            _mapper.Map(trainingDto, training);
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
