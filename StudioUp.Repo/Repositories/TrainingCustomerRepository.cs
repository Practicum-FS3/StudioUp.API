using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog.Filters;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudioUp.Repo.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace StudioUp.Repo.Repositories
{
    public class TrainingCustomerRepository:ITrainingCustomerRepository
    {
        //private AvailableTrainingRepository _availableTrainingRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        

        public TrainingCustomerRepository(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }

        public async Task<List<TrainingCustomerDTO>> GetAllTrainingCustomers()
        {
            try
            {
                var trainingCustomers = await _context.TrainingCustomers.ToListAsync();

                return _mapper.Map<List<TrainingCustomerDTO>>(trainingCustomers);

            }
            catch (Exception e)
            {
                throw e;

            }
        }

        public async Task<TrainingCustomerDTO> GetTraningCustomerById(int id)
        {
            try
            {
                var c = await _context.TrainingCustomers.FirstOrDefaultAsync(t => t.ID == id);
                var mapTrainng = _mapper.Map<TrainingCustomerDTO>(c);
                return mapTrainng;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<TrainingCustomerDTO>> GetTraningCustomerByTraningId(int id)
        {
            try
            {
                var trainingCustomers = await _context.TrainingCustomers.Where(tc => tc.TrainingID == id).ToListAsync();
                return _mapper.Map<List<TrainingCustomerDTO>>(trainingCustomers);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<TrainingCustomerDTO>> GetTraningCustomerByCustomerId(int id)
        {
            try
            {
                var trainingCustomers = await _context.TrainingCustomers.Where(tc => tc.CustomerID == id).ToListAsync();
                return _mapper.Map<List<TrainingCustomerDTO>>(trainingCustomers);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<TrainingCustomerDTO> AddTraningCustomer(TrainingCustomerDTO trainingCustomer)
        {
            try
            {
                var mapCast = _mapper.Map<TrainingCustomer>(trainingCustomer);
                var newTrainingCustomer = await _context.TrainingCustomers.AddAsync(mapCast);
                await _context.SaveChangesAsync();
                trainingCustomer.ID = newTrainingCustomer.Entity.ID;
                return trainingCustomer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateTrainingCustomers(TrainingCustomerDTO trainingCustomer)
        {
            try
            {
                var trainingCustomerToUpdate = await _context.TrainingCustomers.FirstOrDefaultAsync(customerToUpdate => customerToUpdate.ID == trainingCustomer.ID);

                if (trainingCustomerToUpdate == null)
                {
                    return false;
                }

                trainingCustomerToUpdate.TrainingID = trainingCustomer.TrainingID;
                trainingCustomerToUpdate.CustomerID = trainingCustomer.CustomerID;
                trainingCustomerToUpdate.Attended = trainingCustomer.Attended;
                _context.TrainingCustomers.Update(_mapper.Map<TrainingCustomer>(trainingCustomerToUpdate));

                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteTraningCustomer(int id)
        {
            try
            {
                var c = await _context.TrainingCustomers.FirstOrDefaultAsync(t => t.ID == id);
                if (c == null)
                {
                    return false;
                }
                var mapT = _mapper.Map<TrainingCustomer>(c);
                _context.TrainingCustomers.Remove(mapT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<bool> AddTrainingForCustomer(int TrainingId, int CustomerId)
        {
            try
            {
                var training = await _context.AvailableTraining
                .Include(tc => tc.Training)
                .Include(at => at.Training.TrainingCustomerType)
                .FirstOrDefaultAsync(x => x.TrainingId == TrainingId);
                if (training == null)
                    throw new Exception($"cant find training by ID {TrainingId}");

                var customer = await _context.Customers
               .Include(c => c.SubscriptionType)
               .FirstOrDefaultAsync(x => x.Id == CustomerId);
                if (customer == null)
                    throw new Exception($"cant find customer by ID {CustomerId}");

                if (await numOfParticipants(training) && await trainingQuota(customer, training.Date)
                    && await checkType(customer, training))
                {
                    training.ParticipantsCount = training.ParticipantsCount + 1;
                    TrainingCustomerDTO trainingCustomer = new TrainingCustomerDTO();
                    trainingCustomer.TrainingID = TrainingId;
                    trainingCustomer.CustomerID = CustomerId;
                    trainingCustomer.Attended = false;
                    trainingCustomer.IsActive = true;
                    AddTraningCustomer(trainingCustomer);
                    await _context.SaveChangesAsync();
                    throw new Exception($"You have successfully registered for this training");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> trainingQuota(Customer customer, DateOnly date)
        {
            try
            {
                var maxOfTrainingPerWeek = customer.SubscriptionType.NumberOfTrainingPerWeek; //מס' ימי סוג האימון הנוכחי לשבוע
                int currentDayOfWeek = (int)date.DayOfWeek;

                // טווח שבוע האימון הנוכחי
                DateOnly startDate = date.AddDays(-currentDayOfWeek); 
                DateOnly endDate = startDate.AddDays(7); 

                // מס' פעמים ששהה באימון בשבוע של אימון זה
                var numOfTrainingPerWeek = await _context.TrainingCustomers
                .Include(tc => tc.Training)
                .Where(x => x.CustomerID == customer.Id && x.Attended &&
                x.Training.Date >= startDate && x.Training.Date < endDate)
                .CountAsync();

                if (numOfTrainingPerWeek < maxOfTrainingPerWeek)
                {
                    return true;
                }
                throw new Exception($"You have exceeded the amount of training this week");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> numOfParticipants(AvailableTraining training)
        {
            try
            {
                var currentNumOfParticipants = training.ParticipantsCount; //מס' משתתפים נוכחי
                var MaxNumOfParticipants = training.Training.ParticipantsCount; //מס' מקסימלי של משתתפים
                if (currentNumOfParticipants < MaxNumOfParticipants)
                {
                    return true;
                }
                throw new Exception($"The quota of participants for this training is full");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> checkType(Customer customer, AvailableTraining training)
        {
            try
            {
                if (training.Training.TrainingCustomerType.CustomerTypeID == customer.CustomerTypeId)
                {
                    return true;
                }
                throw new Exception($"Your client type is not compatible with this training");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


















        //public async Task<bool> AddTrainingForCustomer(int TrainingId, int CustomerId)
        //{
        //    try
        //    {
        //        var training = await _context.AvailableTraining
        //       .FirstOrDefaultAsync(x => x.TrainingId == TrainingId);
        //        if(training == null)
        //            throw new Exception($"cant find training by ID {TrainingId}");
        //        if (await numOfParticipants(TrainingId) && await trainingQuota(CustomerId,training.Date)
        //            && await checkType(TrainingId, CustomerId))
        //        {
        //            training.ParticipantsCount = training.ParticipantsCount + 1;
        //            TrainingCustomerDTO trainingCustomer = new TrainingCustomerDTO();
        //            trainingCustomer.TrainingID = TrainingId;
        //            trainingCustomer.CustomerID = CustomerId;
        //            trainingCustomer.Attended = false;
        //            trainingCustomer.IsActive = true;
        //            AddTraningCustomer(trainingCustomer);
        //            await _context.SaveChangesAsync();

        //            return true;
        //        }

        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public async Task<bool> trainingQuota(int CustomerId, DateOnly date)
        //{
        //    try
        //    {
        //        var customer = await _context.Customers
        //       .Include(c => c.SubscriptionType)
        //       .FirstOrDefaultAsync(x => x.Id == CustomerId);
        //        if (customer == null)
        //            throw new Exception($"cant find customer by ID {CustomerId}");

        //        //מס' ימי סוג האימון הנוכחי לשבוע
        //        var maxOfTrainingPerWeek = customer.SubscriptionType.NumberOfTrainingPerWeek;

        //        //DateOnly date = DateOnly.FromDateTime(DateTime.Today);
        //        int currentDayOfWeek = (int)date.DayOfWeek;

        //        // טווח השבוע הנוכחי
        //        DateOnly startDate = date.AddDays(-currentDayOfWeek); // Start of the current week
        //        DateOnly endDate = startDate.AddDays(7); // End of the current week

        //        //מס' ימי השבוע ששהה באימון
        //        var numOfTrainingPerWeek = await _context.TrainingCustomers
        //        .Include(tc => tc.Training)
        //        .Where(x => x.CustomerID == CustomerId && x.Attended &&
        //        x.Training.Date >= startDate && x.Training.Date < endDate)
        //        .CountAsync();

        //        if (numOfTrainingPerWeek < maxOfTrainingPerWeek)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<bool> numOfParticipants(int TrainingId)
        //{
        //    try
        //    {
        //        var training = await _context.AvailableTraining
        //        .Include(tc => tc.Training)
        //        .FirstOrDefaultAsync(x => x.TrainingId == TrainingId);

        //        //מס' משתתפים נוכחי
        //        var currentNumOfParticipants = training.ParticipantsCount;
        //        //מס' מקסימלי של משתתפים
        //        var MaxNumOfParticipants = training.Training.ParticipantsCount;
        //        if (currentNumOfParticipants < MaxNumOfParticipants)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public async Task<bool> checkType(int TrainingId, int CustomerId)
        //{
        //    try
        //    {
        //        var customer = await _context.Customers
        //        .FirstOrDefaultAsync(x => x.Id == CustomerId);

        //        var training = await _context.AvailableTraining
        //        .Include(at=> at.Training.TrainingCustomerType)
        //        .FirstOrDefaultAsync(x => x.TrainingId == TrainingId);

        //        if (training.Training.TrainingCustomerType.CustomerTypeID == customer.CustomerTypeId)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}














        //public async Task<bool> AddTrainingForCustomer(int TrainingId,int CustomerId)
        //{
        //    try
        //    {
        //         var trainingCustomer = await _context.TrainingCustomers
        //        .Include(tc => tc.Training)
        //        .FirstOrDefaultAsync(x => x.TrainingID == TrainingId);
        //        if (await numOfParticipants(TrainingId) && await trainingQuota(TrainingId, CustomerId) 
        //            && await checkType(TrainingId, CustomerId))
        //        {
        //            trainingCustomer.Training.ParticipantsCount = trainingCustomer.Training.ParticipantsCount + 1;
        //            TrainingCustomerDTO trainingCustomer1 = new TrainingCustomerDTO();
        //            trainingCustomer1.TrainingID = TrainingId;
        //            trainingCustomer1.CustomerID = CustomerId;
        //            trainingCustomer1.Attended = false;
        //            trainingCustomer1.IsActive = true;
        //            AddTraningCustomer(trainingCustomer1);
        //            await _context.SaveChangesAsync();

        //            return true;
        //        }

        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public async Task<bool> trainingQuota(int TrainingId, int CustomerId)
        //{
        //    try { 
        //         var training = await _context.TrainingCustomers
        //        .Include(tc => tc.Customer.SubscriptionType) 
        //        .FirstOrDefaultAsync(x => x.TrainingID == TrainingId);

        //        //מס' ימי סוג האימון הנוכחי לשבוע
        //        var maxOfTrainingPerWeek = training.Customer.SubscriptionType.NumberOfTrainingPerWeek;

        //        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        //        int currentDayOfWeek = (int)today.DayOfWeek;

        //        // טווח השבוע הנוכחי
        //        DateOnly startDate = today.AddDays(-currentDayOfWeek); // Start of the current week
        //        DateOnly endDate = startDate.AddDays(7); // End of the current week

        //        //מס' ימי השבוע ששהה באימון
        //        var numOfTrainingPerWeek = await _context.TrainingCustomers
        //        .Include(tc => tc.Training)
        //        .Where(x => x.CustomerID == CustomerId && x.Attended &&
        //        x.Training.Date >= startDate && x.Training.Date < endDate)
        //        .CountAsync();

        //        if (numOfTrainingPerWeek < maxOfTrainingPerWeek )
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<bool> numOfParticipants(int TrainingId)
        //{
        //    try { 
        //        var training = await _context.TrainingCustomers
        //        .Include(tc => tc.Training.Training) 
        //        .FirstOrDefaultAsync(x => x.TrainingID == TrainingId);

        //        //מס' משתתפים נוכחי
        //        var currentNumOfParticipants = training.Training.ParticipantsCount;
        //        //מס' מקסימלי של משתתפים
        //        var MaxNumOfParticipants = training.Training.Training.ParticipantsCount;
        //        Console.WriteLine(currentNumOfParticipants);
        //        if (currentNumOfParticipants < MaxNumOfParticipants)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public async Task<bool> checkType(int TrainingId, int CustomerId)
        //{
        //    try
        //    {
        //        var training = await _context.TrainingCustomers
        //        .Include(tc => tc.Customer.CustomerType)
        //        .Include(tc => tc.Training.Training.TrainingCustomerType.TrainingType)
        //        .FirstOrDefaultAsync(x => x.TrainingID == TrainingId);

        //        if (training.Customer.CustomerType.Title==training.Training.Training.TrainingCustomerType.TrainingType.Title)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
 
