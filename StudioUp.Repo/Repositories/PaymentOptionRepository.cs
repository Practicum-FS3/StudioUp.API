using Microsoft.EntityFrameworkCore;
using StudioUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{
    public class PaymentOptionRepository : IRepository<PaymentOption>
    {
        private readonly DataContext _context;

        public PaymentOptionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<PaymentOption>> GetAllAsync()
        {
            return await _context.PaymentOptions.ToListAsync();
        }

        public async Task<PaymentOption> GetByIdAsync(int id)
        {
            return await _context.PaymentOptions.FindAsync(id);
        }

        public async Task AddAsync(PaymentOption paymentOption)
        {
            await _context.PaymentOptions.AddAsync(paymentOption);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PaymentOption paymentOption)
        {
            _context.PaymentOptions.Update(paymentOption);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var paymentOption = await _context.PaymentOptions.FindAsync(id);
            if (paymentOption != null)
            {
                _context.PaymentOptions.Remove(paymentOption);
                await _context.SaveChangesAsync();
            }
        }
    }
}
