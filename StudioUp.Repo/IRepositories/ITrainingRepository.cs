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
        public Task<List<TrainingDTO>> GetAllTrainings();
        public Task<List<CalanderTrainingDTO>> GetAllTrainingsCalender();
        public Task<TrainingDTO> GetTrainingById(int id);
        public Task<TrainingPostDTO> AddTraining(TrainingPostDTO trainingDto);
        public Task<TrainingPostDTO> UpdateTraining(TrainingPostDTO trainingDto,int id);
        public Task DeleteTraining(int id);
    }
}
