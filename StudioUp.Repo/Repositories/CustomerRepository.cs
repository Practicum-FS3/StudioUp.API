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
        public async Task<int> AddAsync(CustomerDTO entity)
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
                var c = await context.Customers.FirstOrDefaultAsync(t => t.Id == id);
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

        public async Task<List<CustomerDTO>> GetAllAsync()
        {
            try
            {
                var l = await context.Customers.ToListAsync();

                return mapper.Map<List<CustomerDTO>>(l);
                
            }
            catch (Exception ex)
            {
                return new List<CustomerDTO>();
            }

        }

        public async Task<CustomerDTO> GetByIdAsync(int id)
        {
            try
            {
                var c = await context.Customers.FirstOrDefaultAsync(t => t.Id == id);
                var mapCust = mapper.Map<CustomerDTO>(c);
                return mapCust;

            }
            catch (Exception ex)
            {
                return new CustomerDTO();
            }
        }

        public async Task<bool> UpdateAsync(CustomerDTO entity)
        {
            try
            {
                var customerToUpdate = await context.Customers.FirstOrDefaultAsync(customerToUpdate => customerToUpdate.Id == entity.Id);

                if (customerToUpdate == null)
                {
                    return false;
                }

                customerToUpdate.Address = entity.Address;
                customerToUpdate.LastName = entity.LastName;
                customerToUpdate.FirstName = entity.FirstName;
                customerToUpdate.PaymentOptionId = entity.PaymentOptionId;
                customerToUpdate.HMOId = entity.HMOId;
                customerToUpdate.CustomerTypeId = entity.CustomerTypeId;
                customerToUpdate.Email = entity.Email;
                //customerToUpdate.IsActive = entity.IsActive;
                customerToUpdate.SubscriptionTypeId = entity.SubscriptionTypeId;
                customerToUpdate.Tel = entity.Tel;
                context.Customers.Update(mapper.Map<Customer>(customerToUpdate));

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

