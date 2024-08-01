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
    public class TrainingCustomerRepository:ITrainingCustomerRepository
    {
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

        
    }
}
