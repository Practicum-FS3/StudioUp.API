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


    public class TrainingCustomerRepository : ITrainingCustomerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;


        public TrainingCustomerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TrainingCustomerDTO>> GetAllTrainingCustomers()
        {
            try
            {
                var trainingCustomers = await _context.TrainingCustomers.Where(t => t.IsActive).ToListAsync();
                return _mapper.Map<List<TrainingCustomerDTO>>(trainingCustomers);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to get the Training Customers List.", ex);
            }
        }
        public async Task<List<CalanderAvailableTrainingDTO>> GetAllRegisteredTrainingsDetailsAsync()
        {
            try
            {
                var startDate = DateOnly.FromDateTime(DateTime.Now.StartOfWeek(DayOfWeek.Sunday));
                var endDate = startDate.AddDays(7);
                var trainings = await _context.TrainingCustomers.Where(x => x.IsActive
                && x.Training.Date >= startDate && x.Training.Date < endDate)
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
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TrainingCustomerDTO> GetTrainingCustomerById(int id)
        {
            try
            {
                var c = await _context.TrainingCustomers.Where(t => t.ID == id && t.IsActive).FirstOrDefaultAsync();
                return _mapper.Map<TrainingCustomerDTO>(c);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to get the Training Type.\", ex);\r\n.", ex);
            }
        }


        public async Task<List<TrainingCustomerDTO>> GetTrainingCustomerByTrainingId(int id)
        {
            try
            {
                var trainingCustomers = await _context.TrainingCustomers.Where(tc => tc.TrainingID == id).ToListAsync();
                return _mapper.Map<List<TrainingCustomerDTO>>(trainingCustomers);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<TrainingCustomerDTO>> GetTrainingCustomerByCustomerId(int id)
        {
            try
            {
                var trainingCustomers = await _context.TrainingCustomers.Where(tc => tc.CustomerID == id).ToListAsync();
                return _mapper.Map<List<TrainingCustomerDTO>>(trainingCustomers);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TrainingCustomerDTO> AddTrainingCustomer(TrainingCustomerDTO trainingCustomer)
        {
            try
            {
                if (trainingCustomer == null)
                {
                    throw new Exception("Training Customer cannot be null");
                }
                var mapCast = _mapper.Map<TrainingCustomer>(trainingCustomer);
                mapCast.IsActive = true;
                var newTrainingCustomer = await _context.TrainingCustomers.AddAsync(mapCast);
                await _context.SaveChangesAsync();
                trainingCustomer.ID = newTrainingCustomer.Entity.ID;
                return trainingCustomer;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to add the Training Customer.", ex);
            }
        }

        public async Task UpdateTrainingCustomer(TrainingCustomerDTO trainingCustomer)
        {
            try
            {
                var trainingCustomerToUpdate = await _context.TrainingCustomers.FirstOrDefaultAsync(customerToUpdate => customerToUpdate.ID == trainingCustomer.ID);

                if (trainingCustomerToUpdate == null)
                {
                    throw new Exception("Training Customer not found.");
                }
                _mapper.Map(trainingCustomer, trainingCustomerToUpdate);
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to update the Training Customer.", ex);
            }
        }

        public async Task DeleteTrainingCustomer(int id)
        {
            try
            {
                var c = await _context.TrainingCustomers.FirstOrDefaultAsync(t => t.ID == id);
                if (c == null || !c.IsActive)
                {
                    return;
                }
                c.IsActive = false;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to delete the Training Customer.", ex);
            }
        }


    }
}
