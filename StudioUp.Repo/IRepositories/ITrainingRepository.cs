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
        public Task<IEnumerable<TrainingDTO>> GetAllTrainings();
        public Task<IEnumerable<CalanderTrainingDTO>> GetAllTrainingsCalender();
        public Task<TrainingDTO> GetTrainingById(int id);
        public Task<TrainingDTO> AddTraining(TrainingDTO trainingDto);
        public Task UpdateTraining(TrainingDTO trainingDto);
        public Task DeleteTraining(int id);

    }
}
