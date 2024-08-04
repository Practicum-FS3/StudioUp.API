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
        //Task<List<DTO.TrainingCustomerTypeDTO>> GetActiveTrainingCustomerTypes();

        Task<TrainingCustomerTypeDTO> GetTrainingCustomerTypeById(int id);
        Task<TrainingCustomerTypeDTO> UpdateTrainingCustomerType(int id, DTO.TrainingCustomerTypeDTO trainingCustomerTypedto);
        Task<DTO.TrainingCustomerTypeDTO> AddTrainingCustomerType(DTO.TrainingCustomerTypeDTO TrainingCustomerType);
        Task <TrainingCustomerTypeDTO> DeleteTrainingCustomerType(int id);
    }
}
