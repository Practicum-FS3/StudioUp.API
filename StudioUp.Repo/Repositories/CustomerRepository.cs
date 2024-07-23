﻿using System;
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
        public async Task<DTO.CustomerDTO> AddAsync(CustomerDTO entity)
        {
            try
            {
                var mapCast = mapper.Map<Models.Customer>(entity);
                var newCustomer = await context.Customers.AddAsync(mapCast);
                await context.SaveChangesAsync();
                entity.Id = newCustomer.Entity.Id;
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var c = await context.Customers.FirstOrDefaultAsync(t => t.Id == id);
                if (c == null)
                {
                    return false;
                }
                var mapC = mapper.Map<Customer>(c);
                context.Customers.Remove(mapC);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

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
                throw ex;

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
                throw ex;
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
                customerToUpdate.IsActive = entity.IsActive;
                customerToUpdate.SubscriptionTypeId = entity.SubscriptionTypeId;
                customerToUpdate.Tel = entity.Tel;
                customerToUpdate.Email = entity.Email;
                context.Customers.Update(mapper.Map<Customer>(customerToUpdate));

                await context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<CustomerDTO>> FilterAsync(CustomerFilterDTO filter)
        {
            var query = context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(filter.FirstName) || !string.IsNullOrEmpty(filter.LastName))
            {
                var firstName = filter.FirstName?.Trim().Replace(" ", "").ToLower();
                var lastName = filter.LastName?.Trim().Replace(" ", "").ToLower();

                query = query.Where(c =>
                    (string.IsNullOrEmpty(firstName) || c.FirstName.ToLower().Replace(" ", "").Contains(firstName)) &&
                    (string.IsNullOrEmpty(lastName) || c.LastName.ToLower().Replace(" ", "").Contains(lastName)));
            }

            if (!string.IsNullOrEmpty(filter.Email))
            {
                var email = filter.Email.Trim().Replace(" ", "").ToLower();
                query = query.Where(c => c.Email.ToLower().Replace(" ", "").Contains(email));
            }

            return await query.Select(c => new CustomerDTO
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Address = c.Address,
                Email = c.Email,
                Tel = c.Tel,
                PaymentOptionId = c.PaymentOptionId,
                HMOId = c.HMOId,
                CustomerTypeId = c.CustomerTypeId,
                SubscriptionTypeId = c.SubscriptionTypeId,
                IsActive = c.IsActive
            }).ToListAsync();
        }




    }
}


