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

