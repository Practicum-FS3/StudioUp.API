using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudioUp.Models;

namespace StudioUp.Repo.IRepositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Customer entity);
        Task<int> AddAsync(Customer entity);
        Task<bool> DeleteAsync(int id);
    }
}
