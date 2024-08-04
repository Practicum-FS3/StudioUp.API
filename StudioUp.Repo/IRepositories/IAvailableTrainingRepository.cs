﻿using StudioUp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface IAvailableTrainingRepository
    {
        Task<IEnumerable<AvailableTrainingDTO>> GetAllAvailableTrainingsAsync();
        Task<AvailableTrainingDTO> GetAvailableTrainingByIdAsync(int id);
        Task<AvailableTrainingDTO> AddAvailableTrainingAsync(AvailableTrainingDTO availableTrainingDTO);
        Task UpdateAvailableTrainingAsync(int id, AvailableTrainingDTO availableTrainingDTO);
        Task DeleteAvailableTrainingAsync(int id);
    }
}
