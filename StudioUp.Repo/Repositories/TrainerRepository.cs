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
namespace StudioUp.Repo.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {


        private readonly DataContext context;
        private readonly IMapper mapper;

        public TrainerRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }
        public async Task<TrainerDTO> AddTrainer(TrainerDTO trainer)
        {
            try
            {
                var trainer1 = await this.context.Trainers.AddAsync(mapper.Map<Trainer>(trainer));
                await this.context.SaveChangesAsync();
                return trainer;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to add a trainer");
            }
        }


        public async Task<bool> DeleteTrainer(int id)
        {
            try
            {
                var c = await context.Trainers.FirstOrDefaultAsync(t => t.ID == id);
                if (c == null)
                {
                    return false;
                }
                var mapT = mapper.Map<Trainer>(c);
                context.Trainers.Remove(c);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;

            }
        }

        public async Task<List<TrainerDTO>> GetAllTrainers()
        {
            try
            {
                var trainers = await context.Trainers.ToListAsync();

                return mapper.Map<List<TrainerDTO>>(trainers);

            }
            catch (Exception e)
            {
                throw e;

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
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> UpdateTrainer(TrainerDTO trainer)
        {
            try
            {
                var trainer1 = await this.context.Trainers.FirstOrDefaultAsync(trainer1 => trainer1.ID == trainer.ID);
                if (trainer1 == null)
                {
                    return false;
                }
                trainer1.Address = trainer.Address;
                trainer1.Tel = trainer.Tel;
                trainer1.Mail = trainer.Mail;
                trainer1.LastName = trainer.LastName;
                trainer1.FirstName = trainer.FirstName;
                trainer1.IsActive = trainer.IsActive;
                context.Trainers.Update(mapper.Map<Trainer>(trainer1));
                await this.context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Update to trainer failed");
            }

        }


    }
}

