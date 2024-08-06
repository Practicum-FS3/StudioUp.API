using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{
    public class LeumitCommimentRepository : ILeumitCommimentsRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public LeumitCommimentRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }
        public async Task AddAsync(LeumitCommitmentsDTO leumitCommitmentsDTO)
        {
            try
            {
                context.LeumitCommitments.Add(mapper.Map<LeumitCommitments>(leumitCommitmentsDTO));
                await context.SaveChangesAsync();
            }
            catch (Exception exeption)
            {
                throw exeption;
            }

        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var LeumitCommitments = await context.LeumitCommitments.FirstOrDefaultAsync(l => l.Id == id);
                if (LeumitCommitments == null)
                {
                    return false;
                }
                context.LeumitCommitments.Remove(LeumitCommitments);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }

        public async Task<List<LeumitCommitmentsDTO>> GetAllAsync()
        {
            try
            {
                var arr = await context.LeumitCommitments.ToListAsync();

                return mapper.Map<List<LeumitCommitmentsDTO>>(arr);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<LeumitCommitmentsDTO> GetByIdAsync(string id)
        {
            try
            {
                var LeumitCommitments = await context.LeumitCommitments.FirstOrDefaultAsync(l => l.Id == id);
                return mapper.Map<LeumitCommitmentsDTO>(LeumitCommitments);

            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        

        public async Task<LeumitCommitmentsDTO> UpdateAsync(LeumitCommitmentsDTO leumitCommitmentsDTO, string id)
        {

            try
            {
                var existingEntity = await context.LeumitCommitments.FindAsync(id);
                if (existingEntity == null)
                {
                    throw new Exception("Entity not found");
                }
                mapper.Map(leumitCommitmentsDTO, existingEntity);
                await context.SaveChangesAsync();
                return leumitCommitmentsDTO;
            }
            catch (Exception exception)
            {
         
                throw exception;
            }

        }

    
    }
}
