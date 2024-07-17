﻿using Microsoft.AspNetCore.Mvc;
using StudioUp.Repo.IRepositories;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly ICustomerRepository CustomerService;

        public CustomerController(ICustomerRepository customerService)
        {
            this.CustomerService = customerService;
        }

        [HttpGet]
        public async Task<List<DTO.CustomerDTO>> GetAllCustomer()
        {
            try
            {
                return await CustomerService.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]

        public async Task<DTO.CustomerDTO> GetCustomerById(int id)
        {
            try
            {
                return await CustomerService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("api/Customer/addCustomer")]
        public async Task<DTO.CustomerDTO> AddCustomer(DTO.CustomerDTO customer)
        {
            try
            {
                return await CustomerService.AddAsync(customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]

        public async Task<bool> UpdateCustomer(DTO.CustomerDTO customer)
        {
            try
            {
                return await CustomerService.UpdateAsync(customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteCustomer(int id)
        {
            try
            {
                return await CustomerService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
