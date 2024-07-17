using Microsoft.AspNetCore.Mvc;
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
                return new List<DTO.CustomerDTO>();
            }
        }

        [HttpGet]
        [Route("/byId")]

        public async Task<DTO.CustomerDTO> GetCustomerById(int id)
        {
            try
            {
                return await CustomerService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                return new DTO.CustomerDTO();
            }
        }

        [HttpPost]
        [Route("/addCustomer")]
        public async Task<int> AddCustomer(DTO.CustomerDTO customer)
        {
            try
            {
                return await CustomerService.AddAsync(customer);
            }
            catch (Exception ex)
            {
                return -1;
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
                return false;
            }
        }

        [HttpDelete]
        public async Task<bool> DeleteCustomer(int id)
        {
            try
            {
                return await CustomerService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
