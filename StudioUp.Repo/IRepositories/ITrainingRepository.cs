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
        public Task<IEnumerable<Training>> GetAllTrainings();
        public Task<Training> GetTrainingById(int id);
        public Task AddTraining(Training training);
        public Task UpdateTraining(Training training);
        public Task DeleteTraining(int id);
    }
}
