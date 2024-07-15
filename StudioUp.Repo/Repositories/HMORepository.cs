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

namespace StudioUp.Repo.Repository
{
    public class HMORepository : IHMORepository
    {

        private readonly DataContext _context;
        private readonly IMapper mapper;

        public HMORepository(DataContext context, IMapper mapper)
        {
            this._context = context;
            this.mapper = mapper;
        }

        public async Task<int> AddAsync(HMODTO hmo)
        {
            try{
                var newHMO = await this._context.HMOs.AddAsync(mapper.Map<HMO>(hmo));
                await this._context.SaveChangesAsync();
                return newHMO.Entity.ID;
            }
            catch (Exception ex) { 
                return 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var hmo = await this._context.HMOs.FirstOrDefaultAsync(h => h.ID == id);
                if (hmo != null)
                {
                    this._context.HMOs.Remove(hmo);
                    await this._context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<HMODTO>> GetAllAsync()
        {
            try
            {
                return mapper.Map<List<HMO>, List<HMODTO>>(await this._context.HMOs.ToListAsync());
            }
            catch (Exception ex)
            {
                return new List<HMODTO>();
            }
        }

        public async Task<HMODTO> GetByIdAsync(int id)
        {
            try
            {
                var h = mapper.Map<HMO , HMODTO>(await _context.HMOs.FirstOrDefaultAsync(x => x.ID == id));
                return h;
            }
            catch (Exception ex)
            {
                return new HMODTO();
            }
        }

        public async Task<bool> UpdateAsync(HMODTO hmo)
        {
            try
            {
                var h = await GetByIdAsync(hmo.ID);
                if (h == null) { 
                    return false;
                }
                h.Title = hmo.Title;
                _context.HMOs.Update(mapper.Map<HMO>(h));
                await this._context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
