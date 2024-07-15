using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;

namespace StudioUp.Repo.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public CustomerRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            
        }
        public async Task<int> AddAsync(CastomerDTO entity)
        {
            try
            {
                var mapCast = mapper.Map<Models.Customer>(entity);
                var newCustomer = await context.Customers.AddAsync(mapCast);
                await context.SaveChangesAsync();
                return newCustomer.Entity.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var c = await GetByIdAsync(id);                  
                var mapC = mapper.Map<Customer>(c);
                context.Customers.Remove(mapC);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<CastomerDTO>> GetAllAsync()
        {
            try
            {
                var l = await context.Customers.ToListAsync();

                return mapper.Map<List<CastomerDTO>>(l);
                
            }
            catch (Exception ex)
            {
                return new List<CastomerDTO>();
            }

        }

        public async Task<CastomerDTO> GetByIdAsync(int id)
        {
            try
            {
                var c = await context.Customers.FirstOrDefaultAsync(t => t.Id == id);
                var mapCust = mapper.Map<CastomerDTO>(c);
                return mapCust;

            }
            catch (Exception ex)
            {
                return new CastomerDTO();
            }
        }

        public async Task<bool> UpdateAsync(CastomerDTO entity)
        {
            try
            {
                var customerToUpdate = await GetByIdAsync(entity.Id);
                if (customerToUpdate == null)
                {
                    return false;
                }
                var mapC = mapper.Map<Customer>(entity);
                context.Customers.Update(mapC);
                await context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

