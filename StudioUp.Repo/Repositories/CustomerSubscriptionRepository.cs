using Microsoft.EntityFrameworkCore;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
            try
            {
                return await _context.CustomerSubscriptions
                    .Include(c => c.Customer)
                    .Include(s => s.SubscriptionType)
                    .Where(s => s.IsActive)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all customer subscriptions.", ex);
            }
        }

        public async Task<IEnumerable<CustomerSubscription>> GetCustomerSubscriptionsByCustomerIdAsync(int customerId)
        {
            try
            {
                return await _context.CustomerSubscriptions
                    .Where(cs => cs.CustomerID == customerId && cs.IsActive)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving subscriptions for customer ID {customerId}.", ex);
            }
        }

        public async Task<CustomerSubscription> GetCustomerSubscriptionByIdAsync(int id)
        {
            try
            {
                var customerSubscription = await _context.CustomerSubscriptions
                    .Where(c => c.ID == id && c.IsActive)
                    .FirstOrDefaultAsync();

                if (customerSubscription == null)
                {
                    throw new Exception($"CustomerSubscription with ID {id} does not exist or is inactive.");
                }

                return customerSubscription;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the subscription with ID {id}.", ex);
            }
        }

        public async Task AddCustomerSubscriptionAsync(CustomerSubscription subscription)
        {
            try
            {
                await _context.CustomerSubscriptions.AddAsync(subscription);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new customer subscription.", ex);
            }
        }

        public async Task UpdateCustomerSubscriptionAsync(CustomerSubscription subscription)
        {
            try
            {
                var existingSubscription = await _context.CustomerSubscriptions.FindAsync(subscription.ID);

                if (existingSubscription == null)
                {
                    throw new Exception($"CustomerSubscription with ID {subscription.ID} does not exist.");
                }

                _context.Entry(existingSubscription).CurrentValues.SetValues(subscription);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the subscription with ID {subscription.ID}.", ex);
            }
        }

        public async Task DeleteCustomerSubscriptionAsync(int id)
        {
            try
            {
                var customerSubscription = await _context.CustomerSubscriptions.FindAsync(id);

                if (customerSubscription == null || !customerSubscription.IsActive)
                {
                    throw new Exception($"CustomerSubscription with ID {id} does not exist or is already inactive.");
                }

                customerSubscription.IsActive = false;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while attempting to delete the subscription with ID {id}.", ex);
            }
        }
    }
}
