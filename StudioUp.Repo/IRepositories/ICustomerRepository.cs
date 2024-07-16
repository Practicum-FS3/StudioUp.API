using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudioUp.DTO;
using StudioUp.Models;

namespace StudioUp.Repo.IRepositories
{
    public interface ICustomerRepository
    {
        Task<List<CastomerDTO>> GetAllAsync();
        Task<CastomerDTO> GetByIdAsync(int id);
        Task<bool> UpdateAsync(CastomerDTO entity);
        Task<int> AddAsync(CastomerDTO entity);
        Task<bool> DeleteAsync(int id);
    }
}

