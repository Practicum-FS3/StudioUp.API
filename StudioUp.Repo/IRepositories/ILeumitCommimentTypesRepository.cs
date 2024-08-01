using StudioUp.DTO;
using StudioUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ILeumitCommimentTypesRepository
    {
        Task<List<LeumitCommimentTypesDTO>> GetAllAsync();
        Task<LeumitCommimentTypesDTO> GetByIdAsync(string id);
        Task<LeumitCommimentTypesDTO> UpdateAsync(LeumitCommimentTypesDTO leumitCommimentTypesDTO,string id);
        Task AddAsync(LeumitCommimentTypesDTO leumitCommimentTypesDTO);
        Task<bool> DeleteAsync(string id);
    }
}
