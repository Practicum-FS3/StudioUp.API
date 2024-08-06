using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repository
{
    public class HMORepository : IHMORepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<HMORepository> _logger;

        public HMORepository(DataContext context, IMapper mapper, ILogger<HMORepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<HMODTO> AddAsync(HMODTO hmo)
        {
            var newHMO = _mapper.Map<HMO>(hmo);
            _context.HMOs.Add(newHMO);
            await _context.SaveChangesAsync();
            return hmo;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var hmo = await this._context.HMOs.FirstOrDefaultAsync(h => h.ID == id);
                if (hmo != null)
                {
                    _context.HMOs.Remove(hmo);
                    await this._context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func DeleteAsync-Repo");
                throw;
            }
        }



        public async Task<List<HMODTO>> GetAllAsync()
        {
            return _mapper.Map<List<HMO>, List<HMODTO>>(await _context.HMOs.ToListAsync());
        }

        public async Task<HMODTO> GetByIdAsync(int id)
        {
            var hmo = await _context.HMOs.FirstOrDefaultAsync(x => x.ID == id);
            return _mapper.Map<HMO, HMODTO>(hmo);
        }

        public async Task<bool> UpdateAsync(HMODTO hmo)
        {
            var h = await _context.HMOs.FirstOrDefaultAsync(h => h.ID == hmo.ID);
            if (h == null)
            {
                return false;
            }

            h.Title = hmo.Title;
            h.IsActive = hmo.IsActive;
            h.ArrangementName = hmo.ArrangementName;
            h.TrainingsPerMonth = hmo.TrainingsPerMonth;
            h.TrainingPrice = hmo.TrainingPrice;
            h.TrainingDescription = hmo.TrainingDescription;
            h.MaximumAge = hmo.MaximumAge;
            h.MinimumAge = hmo.MinimumAge;

            _context.HMOs.Update(h);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
