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
    public class TrainingCustomerTypeRepository : ITrainingCustomerTypeRepository
    {


        private readonly DataContext context;
        private readonly IMapper mapper;

        public TrainingCustomerTypeRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }
        //הוספת אימון כולל בדיקות האם זה אפשרי
        public async Task<DTO.TrainingCustomerTypeDTO> AddTrainingCustomerType(DTO.TrainingCustomerTypeDTO trainingCustomerTypedto)
        {
            var trainingCustomerType = mapper.Map<Models.TrainingCustomerType>(trainingCustomerTypedto);

            TrainingType trainingType = context.TrainingTypes.FirstOrDefault(t => t.ID == trainingCustomerType.TrainingTypeId);
            trainingCustomerType.TrainingType = trainingType;

            CustomerType customerType = context.CustomerTypes.FirstOrDefault(t => t.ID == trainingCustomerType.CustomerTypeID);
            trainingCustomerType.CustomerType = customerType;

            //בדיקות בשביל לראות האם אפשר להוסיף כזה סוג אימון
            if (trainingCustomerType != null && trainingCustomerType.TrainingType != null && trainingCustomerType.CustomerType != null)
            {
               // בדיקה האם סוג האימון בפעילות
                if (trainingCustomerType.TrainingType.IsActive)
                {
                    //האם יש אפשרות לכזה לקוח
                    if (trainingCustomerType.CustomerType.IsActive)
                    {
                        var allTrainingCustomerType = await GetAllTrainingCustomerTypes();
                        foreach (var item in allTrainingCustomerType)
                        {
                            if(item.TrainingTypeID== trainingCustomerType.TrainingTypeId&& item.CustomerTypeID == trainingCustomerType.CustomerTypeID)
                            {
                                throw new Exception("כבר קיים כזה סוג אימון!!");
                            }
                        }
                        var TrainingCustomerType1 = await this.context.TrainingCustomersTypes.AddAsync(mapper.Map<Models.TrainingCustomerType>(trainingCustomerType));
                        await this.context.SaveChangesAsync();
                        return trainingCustomerTypedto;
                    }
                    else {throw new Exception("Condition not met: CustomerType not active");}
                }
                else { throw new Exception("Condition not met: TrainingType not active"); }
            }
            else {throw new Exception("לא נמצא כזה סוג אימון או לקוח, אולי זה לא פעיל!!");}
        }

        //הפונקציה הזו הופכת את ה isActive להיות false
        public async Task<TrainingCustomerTypeDTO> DeleteTrainingCustomerType(int id)
        {
            var thisTCT = await context.TrainingCustomersTypes.FindAsync(id);
            if (thisTCT == null)
                return null;
            thisTCT.IsActive = false;
            context.Entry(thisTCT).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return mapper.Map<TrainingCustomerTypeDTO>(thisTCT);
        }
        public async Task<TrainingCustomerTypeDTO> UpdateTrainingCustomerType(int id, DTO.TrainingCustomerTypeDTO trainingCustomerTypedto)
        {
            var trainingCustomerType = await context.TrainingCustomersTypes.FindAsync(id);
            if (trainingCustomerType == null)
                return null;

            mapper.Map(trainingCustomerTypedto, trainingCustomerType);
            context.Entry(trainingCustomerType).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return mapper.Map<TrainingCustomerTypeDTO>(trainingCustomerType);
        }
        public async Task<List<DTO.TrainingCustomerTypeDTO>> GetAllTrainingCustomerTypes()
        {
            var TrainingCustomerType = await context.TrainingCustomersTypes.ToListAsync();

            return mapper.Map<List<DTO.TrainingCustomerTypeDTO>>(TrainingCustomerType);

        }

        //public async Task<List<DTO.TrainingCustomerTypeDTO>> GetActiveTrainingCustomerTypes()
        //{
        //    var TrainingCustomerTypes = await context.TrainingCustomersTypes.Where(t => t.IsActive == true).ToListAsync();

        //    return mapper.Map<List<DTO.TrainingCustomerTypeDTO>>(TrainingCustomerTypes);
        //}

        public async Task<DTO.TrainingCustomerTypeDTO> GetTrainingCustomerTypeById(int id)
        {
            try
            {
                var c = await context.TrainingCustomersTypes.FirstOrDefaultAsync(t => t.Id == id);
                var mapTrain = mapper.Map<DTO.TrainingCustomerTypeDTO>(c);
                return mapTrain;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
       

    }


}
