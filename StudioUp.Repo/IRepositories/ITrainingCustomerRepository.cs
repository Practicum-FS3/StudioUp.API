using StudioUp.DTO;
using StudioUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ITrainingCustomerRepository
    {
        Task<List<TrainingCustomerDTO>> GetAllTrainingCustomers();
        Task<TrainingCustomerDTO> GetTraningCustomerById(int id);
        Task<List<TrainingCustomerDTO>> GetTraningCustomerByTraningId(int id);
        Task<TrainingCustomerDTO> AddTraningCustomer(TrainingCustomerDTO trainingCustomer);
        Task<bool> UpdateTrainingCustomers(TrainingCustomerDTO trainingCustomer);       
        Task<bool> DeleteTraningCustomer(int id);
    }
}
