using StudioUp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ITrainingCustomerTypeRepository
    {
        Task<List<TrainingCustomerTypeDTO>> GetAllTrainingCustomerTypes();
        //Task<TrainingCustomerTypeDTO> GetTrainingCustomerTypeById(int id);
        //Task<bool> UpdateTrainingCustomerType(TrainingCustomerTypeDTO trainingCustomerType);
        //Task<TrainingCustomerTypeDTO> AddTrainingCustomerType(TrainingCustomerTypeDTO trainingCustomerType);
        //Task<bool> DeleteTrainingCustomerType(int id);
    }
}
