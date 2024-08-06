using Microsoft.EntityFrameworkCore;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{
    public class CustomerSubscriptionRepository : ICustomerSubscriptionRepository
    {
        private readonly DataContext _context;

        public CustomerSubscriptionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerSubscription>> GetAllCustomerSubscriptionsAsync()
        {
            return await _context.CustomerSubscriptions.Include(c => c.Customer).Include(s => s.SubscriptionType).ToListAsync();
        }

        public async Task<IEnumerable<CustomerSubscription>> GetCustomerSubscriptionsByCustomerIdAsync(int customerId)
        {
            return await _context.CustomerSubscriptions
                .Where(cs => cs.CustomerID == customerId)
                .ToListAsync();
        }

        public async Task<CustomerSubscription> GetCustomerSubscriptionByIdAsync(int id)
        {
            return await _context.CustomerSubscriptions.FindAsync(id);
        }

        public async Task AddCustomerSubscriptionAsync(CustomerSubscription subscription)
        {
            await _context.CustomerSubscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerSubscriptionAsync(CustomerSubscription subscription)
        {
            _context.CustomerSubscriptions.Update(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerSubscriptionAsync(int id)
        {
            var subscription = await _context.CustomerSubscriptions.FindAsync(id);
            if (subscription != null)
            {
                _context.CustomerSubscriptions.Remove(subscription);
                await _context.SaveChangesAsync();
            }
        }
    }
}
