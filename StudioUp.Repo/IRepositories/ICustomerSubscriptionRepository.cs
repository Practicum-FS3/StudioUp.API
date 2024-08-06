using StudioUp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ICustomerSubscriptionRepository
    {
        Task<IEnumerable<CustomerSubscription>> GetAllCustomerSubscriptionsAsync();
        Task<CustomerSubscription> GetCustomerSubscriptionByIdAsync(int id);
        Task AddCustomerSubscriptionAsync(CustomerSubscription subscription);
        Task UpdateCustomerSubscriptionAsync(CustomerSubscription subscription);
        Task DeleteCustomerSubscriptionAsync(int id);

        Task<IEnumerable<CustomerSubscription>> GetCustomerSubscriptionsByCustomerIdAsync(int customerId);
    }

}
