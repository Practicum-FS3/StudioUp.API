using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{
    public class SubscriptionTypeRepository : IRepository<SubscriptionTypeDTO>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<SubscriptionTypeRepository> _logger;


        public SubscriptionTypeRepository(DataContext context, IMapper mapper, ILogger<SubscriptionTypeRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<SubscriptionTypeDTO>> GetAllAsync()
        {
            try
            {
                var s = await _context.SubscriptionTypes.ToListAsync();
                return _mapper.Map<List<SubscriptionTypeDTO>>(s);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetAllAsync-Repo");
                throw;
            }

        }

        public async Task<SubscriptionTypeDTO> GetByIdAsync(int id)
        {
            try
            {
                var s = await _context.SubscriptionTypes.FindAsync(id);
                return _mapper.Map<SubscriptionTypeDTO>(s);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetByIdAsync-Repo");
                throw;
            }

        }

        public async Task<SubscriptionTypeDTO> AddAsync(SubscriptionTypeDTO subscriptionType)
        {
            try
            {
                var s = await _context.SubscriptionTypes.AddAsync(_mapper.Map<SubscriptionType>(subscriptionType));
                await _context.SaveChangesAsync();
                return subscriptionType;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func AddAsync-Repo");
                throw;
            }

        }

        public async Task UpdateAsync(SubscriptionTypeDTO subscriptionType)
        {
            try
            {
                _context.SubscriptionTypes.Update(_mapper.Map<SubscriptionType>(subscriptionType));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func UpdateAsync-Repo");
                throw;
            }

        }

        public async Task<bool> DeleteSubscription(int id)
        {
            try
            {
                var subscriptionType = await _context.SubscriptionTypes.FindAsync(id);
                if (subscriptionType == null)
                {
                    throw new Exception($"cant find subscription by ID {id}"); 
                }
                _context.SubscriptionTypes.Remove(subscriptionType);
                    await _context.SaveChangesAsync();
                return true;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func DeleteAsync-Repo");
                throw;
            }
           
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
