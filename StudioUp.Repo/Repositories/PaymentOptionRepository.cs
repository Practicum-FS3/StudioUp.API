using AutoMapper;
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
    public class PaymentOptionRepository : IRepository<PaymentOptionDTO>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentOptionRepository> _logger;


        public PaymentOptionRepository(DataContext context, IMapper mapper, ILogger<PaymentOptionRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<PaymentOptionDTO>> GetAllAsync()
        {
            return _mapper.Map<List<PaymentOptionDTO>>(await _context.PaymentOptions.ToListAsync());
        }

        public async Task<PaymentOptionDTO> GetByIdAsync(int id)
        {
            try
            {
                return _mapper.Map<PaymentOptionDTO>(await _context.PaymentOptions.FindAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetByIdAsync-Repo");
                throw;
            }
        }

        public async Task<PaymentOptionDTO> AddAsync(PaymentOptionDTO paymentOption)
        {
            try
            {
                var p = await _context.PaymentOptions.AddAsync(_mapper.Map<PaymentOption>(paymentOption));
                await _context.SaveChangesAsync();
                return paymentOption;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func AddAsync-Repo");
                throw;
            }

        }

        public async Task UpdateAsync(PaymentOptionDTO paymentOption)
        {
            try
            {
                _context.PaymentOptions.Update(_mapper.Map<PaymentOption>(paymentOption));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func UpdateAsync-Repo");
                throw;
            }

        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var paymentOption = await _context.PaymentOptions.FindAsync(id);
                if (paymentOption == null)
                {
                    throw new Exception($"cant find payment option by ID {id}");
                }
                _context.PaymentOptions.Remove(paymentOption);
                    await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func DeleteAsync-Repo");
                throw;
            }

        }
    }
}
