using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioUp.DTO;
using StudioUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{
    public class CustomerTypeRepository : IRepository<CustomerTypeDTO>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerTypeRepository> _logger;


        public CustomerTypeRepository(DataContext context, IMapper mapper, ILogger<CustomerTypeRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CustomerTypeDTO>> GetAllAsync()
        {
            return _mapper.Map<List<CustomerTypeDTO>>(await _context.CustomerTypes.ToListAsync());
        }

        public async Task<CustomerTypeDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<CustomerTypeDTO>(await _context.CustomerTypes.FindAsync(id));
        }

        public async Task<CustomerTypeDTO> AddAsync(CustomerTypeDTO customerType)
        {
            var cT = customerType;
            await _context.CustomerTypes.AddAsync(_mapper.Map<CustomerType>(customerType));
            await _context.SaveChangesAsync();
            return customerType;
        }

        public async Task UpdateAsync(CustomerTypeDTO customerType)
        {
            _context.CustomerTypes.Update(_mapper.Map<CustomerType>(customerType));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customerType = await _context.CustomerTypes.FindAsync(id);
            if (customerType != null)
            {
                _context.CustomerTypes.Remove(customerType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
