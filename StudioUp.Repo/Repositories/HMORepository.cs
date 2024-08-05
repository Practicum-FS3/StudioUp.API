using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

namespace StudioUp.Repo.Repository
{
    public class HMORepository : IHMORepository
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<HMORepository> _logger;


        public HMORepository(DataContext context, IMapper mapper,ILogger<HMORepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<HMODTO> AddAsync(HMODTO hmo)
        {
            try{
                var newHMO = await this._context.HMOs.AddAsync(_mapper.Map<HMO>(hmo));
                await _context.SaveChangesAsync();
                return hmo;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "- this error in the func AddAsync-Repo");
                throw;
            }
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
            try
            {
                return _mapper.Map<List<HMO>, List<HMODTO>>(await this._context.HMOs.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetAllAsync-Repo");
                throw;
            }
        }

        public async Task<HMODTO> GetByIdAsync(int id)
        {
            try
            {
                var h = _mapper.Map<HMO , HMODTO>(await _context.HMOs.FirstOrDefaultAsync(x => x.ID == id));
                return h;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "- this error in the func GetByIdAsync-Repo");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id , HMODTO hmo)
        {
            try
            {
                var h = await this._context.HMOs.FirstOrDefaultAsync(h => h.ID == id);
                if (h == null) { 

                }
                h.Title = hmo.Title;
                h.IsActive = hmo.IsActive;

                h.ArrangementName = hmo.ArrangementName;

                h.TrainingsPerMonth = hmo.TrainingsPerMonth;
                h.TrainingPrice = hmo.TrainingPrice;

                h.TrainingDescription = hmo.TrainingDescription;

                h.MaximumAge = hmo.MaximumAge;

                h.MinimumAge = hmo.MinimumAge;
                _context.HMOs.Update(_mapper.Map<HMO>(h));
                await this._context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func UpdateAsync-Repo");
                throw;
            }
        }
    }
}
