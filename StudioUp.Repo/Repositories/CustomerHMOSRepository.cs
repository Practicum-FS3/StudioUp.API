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
    public class CustomerHMOSRepository : ICustomerHMOSRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CustomerHMOSRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CustomerHMOSDTO>> GetAllAsync()
        {
            var customersHMOs = await _context.CustomerHMOS.Where(ct => ct.IsActive).ToListAsync();
            return _mapper.Map<IEnumerable<CustomerHMOSDTO>>(customersHMOs);
        }
        public async Task<CustomerHMOSDTO> GetByIdAsync(int id)
        {
            var customerHMOS = await _context.CustomerHMOS.Where(ct => ct.ID == id && ct.IsActive)
                                             .FirstOrDefaultAsync(); 
            return _mapper.Map<CustomerHMOSDTO>(customerHMOS);
        }
        public async Task<int> AddAsync(CustomerHMOSDTO customerHMOSDTO)
        {
            var customerHMOS = _mapper.Map<CustomerHMOS>(customerHMOSDTO);
            customerHMOS.IsActive = true;
            var newCustomer = await _context.CustomerHMOS.AddAsync(customerHMOS);
            await _context.SaveChangesAsync();
            return newCustomer.Entity.ID;
        }
        public async Task UpdateAsync(CustomerHMOSDTO customerHMOSDTO)
        {
            try
            {
                _context.CustomerHMOS.Update(_mapper.Map<CustomerHMOS>(customerHMOSDTO));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                var customerHMO = await _context.CustomerHMOS.FindAsync(id);
                if (customerHMO == null || !customerHMO.IsActive)
                    return;

                customerHMO.IsActive = false;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
