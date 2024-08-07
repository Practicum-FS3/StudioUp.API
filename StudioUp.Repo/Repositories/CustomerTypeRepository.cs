using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class CustomerTypeRepository : ICustomerTypeRepository
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
            var l = await _context.CustomerTypes.Where(ct => ct.IsActive).ToListAsync();

            return _mapper.Map<List<CustomerTypeDTO>>(l);
        }

        public async Task<CustomerTypeDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<CustomerTypeDTO>(await _context.CustomerTypes.Where(ct => ct.ID == id && ct.IsActive)
                                             .FirstOrDefaultAsync());
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
            try
            {
                var customerType = await _context.CustomerTypes.FindAsync(id);
                if (customerType == null || !customerType.IsActive)
                    return;

                customerType.IsActive = false;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex; 
            }

        }
    }
}
