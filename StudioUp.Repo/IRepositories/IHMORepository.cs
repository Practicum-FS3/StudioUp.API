using StudioUp.DTO;
using StudioUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface IHMORepository
    {
        Task<List<HMODTO>> GetAllAsync();

        Task<HMODTO> GetByIdAsync(int id);

        Task<bool> UpdateAsync(HMODTO hmo);

        Task<int> AddAsync(HMODTO hmo);

        Task<bool> DeleteAsync(int id);
    }
}
