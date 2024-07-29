using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{
    public class TrainingCustomerTypeRepository:ITrainingCustomerTypeRepository
    {


        private readonly DataContext context;
        private readonly IMapper mapper;

        public TrainingCustomerTypeRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }
        public async Task<TrainingCustomerTypeDTO> AddTrainingCustomerType(TrainingCustomerTypeDTO TrainingCustomerType)
        {
           
                var TrainingCustomerType1 = await this.context.TrainingCustomersTypes.AddAsync(mapper.Map<TrainingCustomerType>(TrainingCustomerType));
                await this.context.SaveChangesAsync();
                return TrainingCustomerType;
           
        }


        //public async Task<bool> DeleteTrainingCustomerType(int id)
        //{
            
        //}

        public async Task<List<TrainingCustomerTypeDTO>> GetAllTrainingCustomerTypes()
        {
                var TrainingCustomerType = await context.TrainingCustomersTypes.ToListAsync();

                return mapper.Map<List<TrainingCustomerTypeDTO>>(TrainingCustomerType);

        }

        //public async Task<TrainingCustomerTypeDTO> GetTrainingCustomerTypeById(int id)
        //{
        //    try
        //    {
        //        var c = await context.TrainingCustomerType.FirstOrDefaultAsync(t => t.ID == id);
        //        var mapTrain = mapper.Map<TrainingCustomerTypeDTO>(c);
        //        return mapTrain;

        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        //public async Task<bool> UpdateTrainingCustomerType(TrainingCustomerTypeDTO TrainingCustomerType)
        //{
        //    try
        //    {
        //        var TrainingCustomerType1 = await this.context.TrainingCustomerType.FirstOrDefaultAsync(TrainingCustomerType1 => TrainingCustomerType1.ID == TrainingCustomerType.ID);
        //        if (TrainingCustomerType1 == null)
        //        {
        //            return false;
        //        }
        //        TrainingCustomerType1.Address = TrainingCustomerType.Address;
        //        TrainingCustomerType1.Tel = TrainingCustomerType.Tel;
        //        TrainingCustomerType1.Mail = TrainingCustomerType.Mail;
        //        TrainingCustomerType1.LastName = TrainingCustomerType.LastName;
        //        TrainingCustomerType1.FirstName = TrainingCustomerType.FirstName;
        //        TrainingCustomerType1.IsActive = TrainingCustomerType.IsActive;
        //        context.TrainingCustomerTypes.Update(mapper.Map<TrainingCustomerType>(TrainingCustomerType1));
        //        await this.context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Update to TrainingCustomerType failed");
        //    }

        //}

    }
}
