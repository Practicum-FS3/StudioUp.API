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
        public async Task<DTO.TrainingCustomerTypePostComand> AddTrainingCustomerType(DTO.TrainingCustomerTypePostComand trainingCustomerTypedto)
        {


            var trainingCustomerType = mapper.Map<Models.TrainingCustomerType>(trainingCustomerTypedto);



            // Eager loading TrainingType and CustomerType
            trainingCustomerType.TrainingType = await context.TrainingTypes.FirstOrDefaultAsync(t => t.ID == trainingCustomerType.TrainingTypeId);
            trainingCustomerType.CustomerType = await context.CustomerTypes.FirstOrDefaultAsync(t => t.ID == trainingCustomerType.CustomerTypeID);

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
                            if (item.TrainingTypeID == trainingCustomerType.TrainingTypeId && item.CustomerTypeID == trainingCustomerType.CustomerTypeID)
                            {
                                throw new Exception("כבר קיים כזה סוג אימון!!");
                            }
                        }
                        var TrainingCustomerType1 = await this.context.TrainingCustomersTypes.AddAsync(mapper.Map<Models.TrainingCustomerType>(trainingCustomerType));
                        this.context.Entry(TrainingCustomerType1.Entity).State = EntityState.Added; // Set entity state to Added
                        await this.context.SaveChangesAsync();
                        return trainingCustomerTypedto;
                    }
                    else { throw new Exception("Condition not met: CustomerType not active"); }
                }
                else { throw new Exception("Condition not met: TrainingType not active"); }
            }
            else { throw new Exception("לא נמצא כזה סוג אימון או לקוח, אולי זה לא פעיל!!"); }
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
        public async Task<TrainingCustomerTypePostComand> UpdateTrainingCustomerType(int id, DTO.TrainingCustomerTypePostComand trainingCustomerTypedto)
        {
            var trainingCustomerType = await context.TrainingCustomersTypes.FindAsync(id);
            if (trainingCustomerType == null)
                return null;

            mapper.Map(trainingCustomerTypedto, trainingCustomerType);
            context.Entry(trainingCustomerType).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return mapper.Map<TrainingCustomerTypePostComand>(trainingCustomerType);
        }
        public async Task<List<TrainingCustomerTypeDTO>> GetAllTrainingCustomerTypes()
        {
            var TrainingCustomerType = await context.TrainingCustomersTypes.Include(a => a.TrainingType).Include(a => a.CustomerType).ToListAsync();


            return mapper.Map<List<TrainingCustomerTypeDTO>>(TrainingCustomerType);

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
                var c = await context.TrainingCustomersTypes.Include(a => a.TrainingType).Include(a => a.CustomerType).FirstOrDefaultAsync(t => t.Id == id);
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
