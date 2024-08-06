using StudioUp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using StudioUp.DTO;

namespace StudioUp.Repo
{
    public class ContentSectionRepository : IContentSectionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ContentSectionRepository> _logger;


        public ContentSectionRepository(DataContext context, IMapper mapper, ILogger<ContentSectionRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ContentSectionDTO>> GetAllAsync()
        {
            try
            {
                var cS = await _context.ContentSections.Include(cs => cs.ContentType).ToListAsync();
                return _mapper.Map<IEnumerable<ContentSectionDTO>>(cS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetAllAsync-Repo");
                throw;
            }

        }

        public async Task<ContentSectionDTO> GetByIdAsync(int id)
        {
            try
            {
                var contentSection = await _context.ContentSections.Include(cs => cs.ContentType).FirstOrDefaultAsync(cs => cs.ID == id);
                return _mapper.Map<ContentSectionDTO>(contentSection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetByIdAsync-Repo");
                throw;
            }
        }

        public async Task<ContentSectionDTO> AddAsync(ContentSectionDTO contentSection)
        {
            try
            {
                var c = contentSection;
                await _context.ContentSections.AddAsync(_mapper.Map<ContentSection>(contentSection));
                await _context.SaveChangesAsync();
                return contentSection;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func AddAsync-Repo");
                throw;
            }

        }

        public async Task UpdateAsync(ContentSectionDTO contentSection)
        {
            try
            {
                _context.ContentSections.Update(_mapper.Map<ContentSection>(contentSection));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func UpdateAsync-Repo");
                throw;
            }

        }

        public async Task DeleteAsync(ContentSectionDTO contentSection)
        {
            try
            {
                _context.ContentSections.Remove(_mapper.Map<ContentSection>(contentSection));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "- this error in the func DeleteAsync-Repo");
                throw;
            }

        }

        public async Task<IEnumerable<ContentSectionDTO>> GetByContentTypeAsync(int contentTypeId)
        {
            try
            {
                return _mapper.Map<IEnumerable<ContentSectionDTO>>(await _context.ContentSections
                             .Include(cs => cs.ContentType)
                             .Where(cs => cs.ContentTypeID == contentTypeId)
                             .ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetByContentTypeAsync-Repo");
                throw;
            }

        }
    }
}
