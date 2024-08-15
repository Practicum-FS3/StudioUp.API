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
        public TrainerRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<List<TrainerDTO>> GetAllTrainers()
        {
            try
            {
                var x = await context.Trainers.Where(y => y.IsActive).ToListAsync();
                return mapper.Map<List<TrainerDTO>>(x);


            }
            catch
            {
                throw;
            }
        }
        public async Task<TrainerDTO> GetTrainerById(int id)
        {
            try
            {
                var c = await context.Trainers.FirstOrDefaultAsync(t => t.ID == id);
                if (c.IsActive)
                    return mapper.Map<TrainerDTO>(c);
                else
                    throw new Exception($"cant find trainer  by ID {id}");
               
            }
            catch 
            {
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
            catch 
            {
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
            catch 
            {
                throw;
            }
        }
        public async Task DeleteTrainer(int id)
        {
            try
            {
                var c = await context.Trainers.FirstOrDefaultAsync(t => t.ID == id);
                if (c == null||c.IsActive==false)
                {
                    throw new($"Not found trainre with ID: {id}");
                }
                c.IsActive = false;
                await context.SaveChangesAsync();
            }
            catch 
            {
                throw;

            }
        }
    }
}

