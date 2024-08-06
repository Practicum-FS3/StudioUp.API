using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    .Include(x => x.Customer)
                    .Include(x => x.Training)
                    .Include(x => x.Training.Training)

                    .ToListAsync();
                return _mapper.Map<List<CalanderAvailableTrainingDTO>>(trainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetAvailableTrainingsForCustomerAsync-Repo");
                throw;
            }
        }

        public async Task<List<CalanderAvailableTrainingDTO>> FilterAsync(CalanderAvailableTrainingFilterDTO filter)
        {
            try
            {
                var query = _context.TrainingCustomers
                    .Include(x => x.Customer)
                    .Include(x => x.Training)
                    .Include(x => x.Training.Training)
                    .AsQueryable();

                DateOnly today = DateOnly.FromDateTime(DateTime.Now);

                // Filter by past or future trainings
                if (filter.Past.HasValue || filter.Future.HasValue)
                {
                    if (filter.Past.HasValue && filter.Future.HasValue)
                    {
                        // If both are true or both are false, no need to filter by date
                        if (!filter.Past.Value && !filter.Future.Value)
                        {
                            query = query.Where(x => x.Training.Date >= today);
                        }
                        else if (filter.Past.Value && filter.Future.Value)
                        {
                            // No action needed, as we want to include all trainings
                        }
                        else if (filter.Past.Value)
                        {
                            query = query.Where(x => x.Training.Date < today);
                        }
                        else if (filter.Future.Value)
                        {
                            query = query.Where(x => x.Training.Date >= today);
                        }
                    }
                }

                // Filter by date range
                if (filter.StratDate.HasValue && filter.EndDate.HasValue)
                {
                    if (filter.StratDate.Value > filter.EndDate.Value)
                    {
                        throw new ArgumentException("StartDate cannot be greater than EndDate.");
                    }

                    query = query.Where(x => x.Training.Date >= filter.StratDate.Value && x.Training.Date <= filter.EndDate.Value);
                }

                var result = await query.ToListAsync();
                return _mapper.Map<List<CalanderAvailableTrainingDTO>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in FilterAsync");
                throw;
            }
        }




        public async Task<List<CalanderAvailableTrainingDTO>> GetAllCustomersDetailsAsync()
        {
            try
            {
                var startDate = DateOnly.FromDateTime(DateTime.Now.StartOfWeek(DayOfWeek.Sunday));
                var endDate = startDate.AddDays(7);
                var trainings = await _context.TrainingCustomers.Where(x => x.IsActive
                && x.Training.Date >= startDate && x.Training.Date < endDate)
                    .Include(x => x.Customer)
                    .Include(x => x.Training)
                    .Include(x => x.Training.Training)
                    .ToListAsync();

                return _mapper.Map<List<CalanderAvailableTrainingDTO>>(trainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetAvailableTrainingsForCustomerAsync-Repo");
                throw;
            }
        }

    }
    //TODO - find a place
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}

