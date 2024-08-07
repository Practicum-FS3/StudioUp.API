using StudioUp.DTO;
using StudioUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ITrainingRepository
    {
        public Task<IEnumerable<TrainingDTO>> GetAllTrainingsAsync();
        public Task<IEnumerable<CalanderTrainingDTO>> GetAllTrainingsCalenderAsync();
        public Task<TrainingDTO> GetTrainingByIdAsync(int id);
        public Task<TrainingDTO> AddTrainingAsync(TrainingDTO trainingDto);
        public Task<TrainingDTO> UpdateTrainingAsync(TrainingDTO trainingDto,int id);
        public Task DeleteTrainingAsync(int id);
    }
}
