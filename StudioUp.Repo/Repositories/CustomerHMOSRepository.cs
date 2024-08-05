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
            var customersHMOs = await _context.CustomerHMOS.ToListAsync();
            return _mapper.Map<IEnumerable<CustomerHMOSDTO>>(customersHMOs);
        }
        public async Task<CustomerHMOSDTO> GetByIdAsync(int id)
        {
            var customerHMOS = await _context.CustomerHMOS.FindAsync(id);
            if (customerHMOS == null || !customerHMOS.IsActive)
                return null;
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
        public async Task UpdateAsync(int id, CustomerHMOSDTO customerHMOSDTO)
        {
            var customerHMOS = await _context.CustomerHMOS.FindAsync(id);
            _mapper.Map(customerHMOSDTO, customerHMOS);
            _context.Entry(customerHMOS).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
        public async Task<bool> DeleteAsync(int id)
        {
            var customerHMOS = await _context.CustomerHMOS.FindAsync(id);
            if (customerHMOS == null || !customerHMOS.IsActive)
                return false;
            customerHMOS.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
