using Microsoft.EntityFrameworkCore;
using StudioUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{
    public class CustomerTypeRepository : IRepository<CustomerType>
    {
        private readonly DataContext _context;

        public CustomerTypeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerType>> GetAllAsync()
        {
            return await _context.CustomerTypes.ToListAsync();
        }

        public async Task<CustomerType> GetByIdAsync(int id)
        {
            return await _context.CustomerTypes.FindAsync(id);
        }

        public async Task AddAsync(CustomerType customerType)
        {
            await _context.CustomerTypes.AddAsync(customerType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CustomerType customerType)
        {
            _context.CustomerTypes.Update(customerType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customerType = await _context.CustomerTypes.FindAsync(id);
            if (customerType != null)
            {
                _context.CustomerTypes.Remove(customerType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
