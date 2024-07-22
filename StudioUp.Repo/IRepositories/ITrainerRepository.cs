using StudioUp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ITrainerRepository
    {
        Task<List<TrainerDTO>> GetAllTrainers();
        Task<TrainerDTO> GetTrainerById(int id);
        Task<bool> UpdateTrainer(TrainerDTO trainer);
        Task<TrainerDTO> AddTrainer(TrainerDTO trainer);
        Task<bool> DeleteTrainer(int id);
    }
}
