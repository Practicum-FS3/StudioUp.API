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
        Task<AvailableTrainingDTO> GetAvailableTrainingByIdAsync(int id);
        Task AddAvailableTrainingAsync(AvailableTrainingDTO availableTrainingDTO);
        Task UpdateAvailableTrainingAsync(AvailableTrainingDTO availableTrainingDTO);
        Task DeleteAvailableTrainingAsync(int id);
    }
}
