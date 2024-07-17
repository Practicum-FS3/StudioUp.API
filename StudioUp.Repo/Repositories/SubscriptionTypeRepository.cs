using Microsoft.EntityFrameworkCore;
using StudioUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{
    public class SubscriptionTypeRepository : IRepository<SubscriptionType>
    {
        private readonly DataContext _context;

        public SubscriptionTypeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<SubscriptionType>> GetAllAsync()
        {
            return await _context.SubscriptionTypes.ToListAsync();
        }

        public async Task<SubscriptionType> GetByIdAsync(int id)
        {
            return await _context.SubscriptionTypes.FindAsync(id);
        }

        public async Task AddAsync(SubscriptionType subscriptionType)
        {
            await _context.SubscriptionTypes.AddAsync(subscriptionType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SubscriptionType subscriptionType)
        {
            _context.SubscriptionTypes.Update(subscriptionType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var subscriptionType = await _context.SubscriptionTypes.FindAsync(id);
            if (subscriptionType != null)
            {
                _context.SubscriptionTypes.Remove(subscriptionType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
