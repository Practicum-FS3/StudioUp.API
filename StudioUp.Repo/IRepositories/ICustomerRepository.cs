﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudioUp.DTO;
using StudioUp.Models;

namespace StudioUp.Repo.IRepositories
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDTO>> GetAllAsync();
        Task<CustomerDTO> GetByIdAsync(int id);
        Task<CustomerDTO> GetCustomerByEmailAndPassword(string email, string password);

        Task<bool> UpdateAsync(CustomerDTO entity);

        Task<CustomerDTO> AddAsync(CustomerDTO entity);
        Task<bool> DeleteAsync(int id);
    }
}

