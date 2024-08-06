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
    public class LeumitCommimentTypesRepository : ILeumitCommimentTypesRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public LeumitCommimentTypesRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }

        public async Task AddAsync(LeumitCommimentTypesDTO leumitCommimentTypesDTO)
        {
            try
            {
                context.LeumitCommimentTypes.Add(mapper.Map<LeumitCommimentTypes> (leumitCommimentTypesDTO));
                await context.SaveChangesAsync();
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }



        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var leumitCommitmentsType = await context.LeumitCommimentTypes.FindAsync(id);
                if (leumitCommitmentsType == null)
                {
                    return false;
                }
                context.LeumitCommimentTypes.Remove(leumitCommitmentsType);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<List<LeumitCommimentTypesDTO>> GetAllAsync()
        {
            try
            {
                var arr = await context.LeumitCommimentTypes.ToListAsync();

                return mapper.Map<List<LeumitCommimentTypesDTO>>(arr);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<LeumitCommimentTypesDTO> GetByIdAsync(int id)
        {
            try
            {
                return mapper.Map<LeumitCommimentTypesDTO>(await context.LeumitCommimentTypes.FindAsync(id));
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }

        public async Task<LeumitCommimentTypesDTO> UpdateAsync(LeumitCommimentTypesDTO leumitCommimentTypesDTO, int id)
        {
            var existingEntity = await context.LeumitCommimentTypes.FindAsync(id);
            if (existingEntity == null)
            {
                throw new Exception("Entity not found");
            }
            mapper.Map(leumitCommimentTypesDTO, existingEntity);
            await context.SaveChangesAsync();
            return leumitCommimentTypesDTO;
        }
    }
}
