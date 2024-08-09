using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioUp.DTO;
using StudioUp.Models;

namespace StudioUp.Repo.Repositories
{
    public class CustomerTrainingsDetailsRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerTrainingsDetailsRepository> _logger;

        public CustomerTrainingsDetailsRepository(DataContext context, IMapper mapper, ILogger<CustomerTrainingsDetailsRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CalanderAvailableTrainingDTO>> GetDetailsForCustomerAsync(int customerId)
        {
            try
            {
                var startDate = DateOnly.FromDateTime(DateTime.Now.StartOfWeek(DayOfWeek.Sunday));
                var endDate = startDate.AddDays(7);
                var trainings = await _context.TrainingCustomers.Where(x => x.CustomerID == customerId
                    && x.Training.Date >= startDate && x.Training.Date <= endDate
                    && x.IsActive)
                    .Include(tc => tc.Customer)
                        .ThenInclude(c => c.CustomerType)
                    .Include(tc => tc.Training)
                        .ThenInclude(at => at.Training)
                            .ThenInclude(t => t.Trainer)
                    .Include(tc => tc.Training)
                        .ThenInclude(at => at.Training)
                            .ThenInclude(t => t.TrainingCustomerType)
                                .ThenInclude(tct => tct.TrainingType)
                    .ToListAsync();

                return _mapper.Map<List<CalanderAvailableTrainingDTO>>(trainings);
            }
            catch
            {
                throw;
            }
        }

        //public async Task<List<CalanderAvailableTrainingDTO>> FilterAsync(CalanderAvailableTrainingFilterDTO filter)
        //{
        //    try
        //    {
        //        var query = _context.TrainingCustomers
        //       .Include(x => x.Customer)
        //       .ThenInclude(c => c.CustomerType)
        //       .Include(x => x.Training)
        //       .ThenInclude(t => t.Training)
        //       .ThenInclude(tr => tr.Trainer)
        //       .Include(x => x.Training)
        //       .ThenInclude(t => t.Training)
        //       .ThenInclude(t => t.TrainingCustomerType)
        //       .ThenInclude(tct => tct.TrainingType)
        //       .AsQueryable();

        //        var queryTraininigs=_context.Trainings.AsQueryable();

        //        DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);
        //        DateTime todayDateTime = DateTime.Now;
        //        //error
        //        if (filter.StratDate.HasValue && filter.EndDate.HasValue && filter.StratDate.Value > filter.EndDate.Value)
        //        {
        //            throw new ArgumentException("StartDate cannot be greater than EndDate.");
        //        }
        //        //all
        //        if (filter.Future.HasValue && filter.Past.HasValue && filter.Past.Value == filter.Future.Value)
        //        {
        //            //all
        //            if (!filter.StratDate.HasValue && !filter.EndDate.HasValue)
        //            {

        //            }
        //            //range between startDate to endDate
        //            else if (filter.StratDate.HasValue && filter.EndDate.HasValue)
        //            {
        //                query = query.Where(x => x.Training.Date >= filter.StratDate.Value && x.Training.Date <= filter.EndDate.Value);

        //            }
        //            //range from start etc
        //            else if (filter.StratDate.HasValue)
        //            {
        //                query = query.Where(x => x.Training.Date >= filter.StratDate.Value);

        //            }
        //            //range until end etc
        //            else if (filter.EndDate.HasValue)
        //            {
        //                query = query.Where(x => x.Training.Date <= filter.EndDate.Value);

        //            }

        //        }
        //        //past
        //        else if(filter.Future.HasValue && filter.Past.HasValue && filter.Past.Value && !filter.Future.Value)
        //        {
        //            //all in past
        //            if (!filter.StratDate.HasValue && !filter.EndDate.HasValue)
        //            {
        //                query = query.Where(x => x.Training.Date <= todayDate);
        //            }
        //            //range between startDate to endDate in the past
        //            else if (filter.StratDate.HasValue && filter.EndDate.HasValue)
        //            {
        //                query = query.Where(x => x.Training.Date >= filter.StratDate.Value && x.Training.Date <= filter.EndDate.Value && x.Training.Date <= todayDate);

        //            }
        //            //range from start etc in the past
        //            else if (filter.StratDate.HasValue)
        //            {
        //                query = query.Where(x => x.Training.Date >= filter.StratDate.Value && x.Training.Date <= todayDate);

        //            }
        //            //range until end etc in the past
        //            else if (filter.EndDate.HasValue)
        //            {
        //                query = query.Where(x => x.Training.Date <= filter.EndDate.Value && x.Training.Date <= todayDate);

        //            }
        //            query = query.Where(x => x.Training.Date != todayDate || 
        //            (x.Training.Date == todayDate && 
        //            (queryTraininigs.FirstOrDefault(t => t.ID == x.TrainingID)).Hour < todayDateTime.Hour || 
        //            ((queryTraininigs.FirstOrDefault(t => t.ID == x.TrainingID)).Hour == todayDateTime.Hour && 
        //            (queryTraininigs.FirstOrDefault(t => t.ID == x.TrainingID)).Minute <= todayDateTime.Minute)));

        //        }
        //        //future
        //        else
        //        {
        //            //all in future
        //            if (!filter.StratDate.HasValue && !filter.EndDate.HasValue)
        //            {
        //                query = query.Where(x => x.Training.Date >= todayDate);
        //            }
        //            //range between startDate to endDate in the future
        //            else if (filter.StratDate.HasValue && filter.EndDate.HasValue)
        //            {
        //                query = query.Where(x => x.Training.Date >= filter.StratDate.Value && x.Training.Date <= filter.EndDate.Value && x.Training.Date >= todayDate);

        //            }
        //            //range from start etc in the future
        //            else if (filter.StratDate.HasValue)
        //            {
        //                query = query.Where(x => x.Training.Date >= filter.StratDate.Value && x.Training.Date >= todayDate);

        //            }
        //            //range until end etc in the future
        //            else if (filter.EndDate.HasValue)
        //            {
        //                query = query.Where(x => x.Training.Date <= filter.EndDate.Value && x.Training.Date >= todayDate);

        //            }
        //            query = query.Where(x => x.Training.Date != todayDate ||
        //           (x.Training.Date == todayDate &&
        //           (queryTraininigs.FirstOrDefault(t => t.ID == x.TrainingID)).Hour > todayDateTime.Hour ||
        //           ((queryTraininigs.FirstOrDefault(t => t.ID == x.TrainingID)).Hour == todayDateTime.Hour &&
        //           (queryTraininigs.FirstOrDefault(t => t.ID == x.TrainingID)).Minute >= todayDateTime.Minute)));
        //        }



        //        var result = await query.Where(ct => ct.IsActive).ToListAsync();                

        //        return _mapper.Map<List<CalanderAvailableTrainingDTO>>(result);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public async Task<List<CalanderAvailableTrainingDTO>> FilterAsync(CalanderAvailableTrainingFilterDTO filter)
        {
            try
            {
                var query = _context.TrainingCustomers
                    .Include(x => x.Customer)
                        .ThenInclude(c => c.CustomerType)
                    .Include(x => x.Training)
                        .ThenInclude(t => t.Training)
                        .ThenInclude(tr => tr.Trainer)
                    .Include(x => x.Training)
                        .ThenInclude(t => t.Training)
                        .ThenInclude(t => t.TrainingCustomerType)
                        .ThenInclude(tct => tct.TrainingType)
                    .AsQueryable();

                var queryTraininigs = _context.Trainings.AsQueryable();

                DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);
                DateTime todayDateTime = DateTime.Now;

                if (filter.StratDate.HasValue && filter.EndDate.HasValue && filter.StratDate.Value > filter.EndDate.Value)
                {
                    throw new ArgumentException("StartDate cannot be greater than EndDate.");
                }

                if (filter.Future.HasValue && filter.Past.HasValue)
                {
                    // כל האימונים (עבר ועתיד), ללא תאריכים מוגדרים
                    if (filter.Past.Value == filter.Future.Value && !filter.StratDate.HasValue && !filter.EndDate.HasValue)
                    {
                        // ללא פילטרים נוספים
                    }
                    // כל האימונים בטווח תאריכים מוגדר
                    else if (filter.Past.Value == filter.Future.Value && filter.StratDate.HasValue && filter.EndDate.HasValue)
                    {
                        ApplyDateRangeFilter(ref query, filter.StratDate, filter.EndDate);
                    }
                    // כל האימונים מהתחלה מוגדרת (עד תאריך סיום כלשהו)
                    else if (filter.Past.Value == filter.Future.Value && filter.StratDate.HasValue)
                    {
                        ApplyDateRangeFilter(ref query, filter.StratDate, null);
                    }
                    // כל האימונים עד תאריך סיום מוגדר
                    else if (filter.Past.Value == filter.Future.Value && filter.EndDate.HasValue)
                    {
                        ApplyDateRangeFilter(ref query, null, filter.EndDate);
                    }
                    // אימונים בעבר בלבד, ללא תאריכים מוגדרים
                    else if (filter.Past.Value && !filter.Future.Value && !filter.StratDate.HasValue && !filter.EndDate.HasValue)
                    {
                        query = query.Where(x => x.Training.Date <= todayDate);
                    }
                    // אימונים בעבר בלבד בטווח תאריכים מוגדר
                    else if (filter.Past.Value && !filter.Future.Value && filter.StratDate.HasValue && filter.EndDate.HasValue)
                    {
                        query = query.Where(x => x.Training.Date >= filter.StratDate.Value && x.Training.Date <= filter.EndDate.Value && x.Training.Date <= todayDate);
                    }
                    // אימונים בעבר בלבד מהתחלה מוגדרת
                    else if (filter.Past.Value && !filter.Future.Value && filter.StratDate.HasValue)
                    {
                        query = query.Where(x => x.Training.Date >= filter.StratDate.Value && x.Training.Date <= todayDate);
                    }
                    // אימונים בעבר בלבד עד תאריך סיום מוגדר
                    else if (filter.Past.Value && !filter.Future.Value && filter.EndDate.HasValue)
                    {
                        query = query.Where(x => x.Training.Date <= filter.EndDate.Value && x.Training.Date <= todayDate);
                    }
                    // אימונים בעתיד בלבד, ללא תאריכים מוגדרים
                    else if (filter.Future.Value && !filter.Past.Value && !filter.StratDate.HasValue && !filter.EndDate.HasValue)
                    {
                        query = query.Where(x => x.Training.Date >= todayDate);
                    }
                    // אימונים בעתיד בלבד בטווח תאריכים מוגדר
                    else if (filter.Future.Value && !filter.Past.Value && filter.StratDate.HasValue && filter.EndDate.HasValue)
                    {
                        query = query.Where(x => x.Training.Date >= filter.StratDate.Value && x.Training.Date <= filter.EndDate.Value && x.Training.Date >= todayDate);
                    }
                    // אימונים בעתיד בלבד מהתחלה מוגדרת
                    else if (filter.Future.Value && !filter.Past.Value && filter.StratDate.HasValue)
                    {
                        query = query.Where(x => x.Training.Date >= filter.StratDate.Value && x.Training.Date >= todayDate);
                    }
                    // אימונים בעתיד בלבד עד תאריך סיום מוגדר
                    else if (filter.Future.Value && !filter.Past.Value && filter.EndDate.HasValue)
                    {
                        query = query.Where(x => x.Training.Date <= filter.EndDate.Value && x.Training.Date >= todayDate);
                    }
                    // בדיקה לפי שעה ודקה אם התאריך הוא היום (עבר)
                    if (filter.Past.Value && !filter.Future.Value)
                    {
                        query = query.Where(x =>
                            x.Training.Date < todayDate ||
                            (x.Training.Date == todayDate &&
                            queryTraininigs.Any(t => t.ID == x.TrainingID &&
                            (t.Hour < todayDateTime.Hour ||
                            (t.Hour == todayDateTime.Hour && t.Minute <= todayDateTime.Minute)))));
                    }
                    // בדיקה לפי שעה ודקה אם התאריך הוא היום (עתיד)
                    else if (filter.Future.Value && !filter.Past.Value)
                    {
                        query = query.Where(x =>
                            x.Training.Date > todayDate ||
                            (x.Training.Date == todayDate &&
                            queryTraininigs.Any(t => t.ID == x.TrainingID &&
                            (t.Hour > todayDateTime.Hour ||
                            (t.Hour == todayDateTime.Hour && t.Minute >= todayDateTime.Minute)))));
                    }

                }

                var result = await query.Where(ct => ct.IsActive).ToListAsync();
                return _mapper.Map<List<CalanderAvailableTrainingDTO>>(result);
            }
            catch
            {
                throw;
            }
        }

        private void ApplyDateRangeFilter(ref IQueryable<TrainingCustomer> query, DateOnly? startDate, DateOnly? endDate, bool isPast = false)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(x => x.Training.Date >= startDate.Value && x.Training.Date <= endDate.Value);
                if (isPast) query = query.Where(x => x.Training.Date <= DateOnly.FromDateTime(DateTime.Now));
            }
            else if (startDate.HasValue)
            {
                query = query.Where(x => x.Training.Date >= startDate.Value);
                if (isPast) query = query.Where(x => x.Training.Date <= DateOnly.FromDateTime(DateTime.Now));
            }
            else if (endDate.HasValue)
            {
                query = query.Where(x => x.Training.Date <= endDate.Value);
                if (isPast) query = query.Where(x => x.Training.Date <= DateOnly.FromDateTime(DateTime.Now));
            }
        }




        public async Task<List<CalanderAvailableTrainingDTO>> GetAllCustomersDetailsAsync()
        {
            try
            {
                var trainings = await _context.TrainingCustomers.Where(x => x.IsActive)
                    .Include(tc => tc.Customer)
                        .ThenInclude(c => c.CustomerType)
                    .Include(tc => tc.Training)
                        .ThenInclude(at => at.Training)
                            .ThenInclude(t => t.Trainer)
                    .Include(tc => tc.Training)
                        .ThenInclude(at => at.Training)
                            .ThenInclude(t => t.TrainingCustomerType)
                                .ThenInclude(tct => tct.TrainingType)
                    .Where(ct => ct.IsActive).ToListAsync();

                return _mapper.Map<List<CalanderAvailableTrainingDTO>>(trainings);
            }
            catch
            {
                throw;
            }
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
