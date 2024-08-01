using StudioUp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface IAvailableTrainingRepository
    {
        Task<IEnumerable<AvailableTrainingDTO>> GetAllAvailableTrainingsAsync();
        Task<IEnumerable<CalanderAvailableTrainingDTO>> GetAllAvailableTrainingsAsyncForCalander();
        Task<AvailableTrainingDTO> GetAvailableTrainingByIdAsync(int id);
        Task<AvailableTrainingDTO> AddAvailableTrainingAsync(AvailableTrainingDTO availableTrainingDTO);
        Task<AvailableTrainingDTO> UpdateAvailableTrainingAsync(int id, AvailableTrainingDTO availableTrainingDTO);
        Task<bool> DeleteAvailableTrainingAsync(int id);
    }
}
