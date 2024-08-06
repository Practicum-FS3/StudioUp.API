using AutoMapper;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioUp.Repo.Repository;
namespace StudioUp.Repo.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {


        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly ILogger<TrainerRepository> _logger;


        public TrainerRepository(DataContext context, IMapper mapper, ILogger<TrainerRepository> logger)
        {
            this.context = context;
            this.mapper = mapper;
            _logger = logger;
        }
        public async Task<List<TrainerDTO>> GetAllTrainers()
        {
            try
            {
                var trainers = await context.Trainers.ToListAsync();
                return mapper.Map<List<TrainerDTO>>(trainers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetAllTrainers-Repo");
                throw;
            }
        }

        public async Task<TrainerDTO> GetTrainerById(int id)
        {
            try
            {
                var c = await context.Trainers.FirstOrDefaultAsync(t => t.ID == id);
                var mapTrain = mapper.Map<TrainerDTO>(c);
                return mapTrain;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetTrainerById-Repo");
                throw;
            }
        }
        public async Task<TrainerDTO> AddTrainer(TrainerDTO t)
        {
            try
            {
                var trainer = await context.Trainers.AddAsync(mapper.Map<Trainer>(t));
                await this.context.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func AddTrainer-Repo");
                throw;
            }
        }
        public async Task UpdateTrainer(TrainerDTO t)
        {
            try
            {
                var trainer = await this.context.Trainers.FirstOrDefaultAsync(trainer => trainer.ID == t.ID);
                trainer.Address = t.Address;
                trainer.Tel = t.Tel;
                trainer.Mail = t.Mail;
                trainer.LastName = t.LastName;
                trainer.FirstName = t.FirstName;
                trainer.IsActive = t.IsActive;
                context.Trainers.Update(mapper.Map<Trainer>(trainer));
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func UpdateTrainer-Repo");
                throw;
            }

        }
        public async Task DeleteTrainer(int id)
        {
            try
            {
                var c = await context.Trainers.FirstOrDefaultAsync(t => t.ID == id);
                if (c == null)
                {
                    _logger.LogError($"Not found trainre with ID: {id}");
                }
                var mapT = mapper.Map<Trainer>(c);
                context.Trainers.Remove(c);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func DeleteTrainer-Repo");
                throw;

            }
        }
    }
}

