using StudioUp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using StudioUp.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IO;

namespace StudioUp.Repo
{
    public class ContentSectionRepository : IContentSectionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ContentSectionRepository> _logger;


        public ContentSectionRepository(DataContext context, IMapper mapper, ILogger<ContentSectionRepository> logger
            )
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ContentSectionDowoladDTO>> GetAllAsync()
        {
            try
            {
                var cS = await _context.ContentSections.Include(cs => cs.ContentType).Where(cS => cS.IsActive).ToListAsync();
                return _mapper.Map<IEnumerable<ContentSectionDowoladDTO>>(cS);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<ContentSectionDowoladDTO> GetByIdAsync(int id)
        {
            try
            {
                var contentSection = await _context.ContentSections.Include(cs => cs.ContentType).FirstOrDefaultAsync(cs => cs.ID == id && cs.IsActive);
                return _mapper.Map<ContentSectionDowoladDTO>(contentSection);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ContentSectionDowoladDTO> AddAsync(ContentSectionUploadDTO contentSection)
        {
            try
            {
               var c= await _context.ContentSections.AddAsync(_mapper.Map<ContentSection>(contentSection));
                await _context.SaveChangesAsync();
                return _mapper.Map<ContentSectionDowoladDTO>(contentSection);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task UpdateAsync(ContentSectionUploadDTO contentSection)
        {
            try
            {
                _context.ContentSections.Update(_mapper.Map<ContentSection>(contentSection));
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task DeleteAsync(int ID)
        {
            try
            {
                var contentSectionToDelete = await _context.ContentSections
                    .FindAsync(ID);

                if (contentSectionToDelete == null)
                {
                    throw new Exception($"can't delete content section with id:{ID}");
                }
                contentSectionToDelete.IsActive = false;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ContentSectionDowoladDTO>> GetByContentTypeAsync(int contentTypeId)
        {
            try
            {
                return _mapper.Map<IEnumerable<ContentSectionDowoladDTO>>(await _context.ContentSections
                             .Include(cs => cs.ContentType)
                             .Where(cs => cs.ContentTypeID == contentTypeId)
                             .ToListAsync());
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
