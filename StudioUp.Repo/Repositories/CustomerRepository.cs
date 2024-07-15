using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;

namespace StudioUp.Repo.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext context;
        public async Task<int> AddAsync(Customer entity)
        {
            try
            {
                var newCustomer = await context.Customers.AddAsync(entity);
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
                context.Customers.Remove(c);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            try
            {
                return await context.Customers.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<Customer>();
            }

        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            try
            {
                var c = await context.Customers.FirstOrDefaultAsync(t => t.Id == id);
                return c;

            }
            catch (Exception ex)
            {
                return new Customer();
            }
        }

        public async Task<bool> UpdateAsync(Customer entity)
        {
            try
            {
                var customerToUpdate = await GetByIdAsync(entity.Id);
                if (customerToUpdate == null)
                {
                    return false;
                }
                context.Customers.Update(customerToUpdate);
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
