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
    public class SubscriptionTypeRepository : ISubscriptionTypeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SubscriptionTypeRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubscriptionTypeDTO>> GetAllSubscriptions()
        {
            List<SubscriptionType>lst = await _context.SubscriptionTypes.Where(x=> x.IsActive==true).ToListAsync();
            return _mapper.Map<List<SubscriptionTypeDTO>>(lst);
        }

        public async Task<SubscriptionTypeDTO> GetSubscriptionById(int id)
        {
            SubscriptionType s= await _context.SubscriptionTypes.FindAsync(id);
            return _mapper.Map<SubscriptionTypeDTO>(s);
        }

        public async Task<SubscriptionTypeDTO> AddSubscription(SubscriptionTypeDTO subscriptionTypeDto)
        {
            SubscriptionType sub=_mapper.Map<SubscriptionType>(subscriptionTypeDto);
            await _context.SubscriptionTypes.AddAsync(sub);
            await _context.SaveChangesAsync();
            return subscriptionTypeDto;
        }

        public async Task<SubscriptionTypeDTO> UpdateSubscription(SubscriptionTypeDTO subscriptionTypeDto, int id)
        {
            var subscriptionType = await _context.SubscriptionTypes.FindAsync(id);
            if (subscriptionType == null)
                return null; // או throw new NotFoundException(); 
            _mapper.Map(subscriptionTypeDto, subscriptionType);
            _context.SubscriptionTypes.Update(subscriptionType);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubscriptionTypeDTO>(subscriptionType);
        }

        public async Task<SubscriptionTypeDTO> DeleteSubscription(int id)
        {
            var subscriptionType = await _context.SubscriptionTypes.FindAsync(id);
            if (subscriptionType != null)
            {
                subscriptionType.IsActive = false;
                _context.SubscriptionTypes.Update(subscriptionType);
                await _context.SaveChangesAsync();
            }
              return _mapper.Map<SubscriptionTypeDTO>(subscriptionType); ;
        }
    }
}
