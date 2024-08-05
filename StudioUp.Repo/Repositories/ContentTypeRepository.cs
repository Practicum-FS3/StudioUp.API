using StudioUp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using StudioUp.DTO;

namespace StudioUp.Repo
{
    public class ContentTypeRepository : IContentTypeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        private readonly ILogger<ContentTypeRepository> _logger;

        public ContentTypeRepository(DataContext context, IMapper mapper, ILogger<ContentTypeRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ContentTypeDTO>> GetAll()
        {
            try
            {
                var cT = await _context.ContentTypes.ToListAsync();
                return _mapper.Map<IEnumerable<ContentTypeDTO>>(cT);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetAll-Repo");
                throw;
            }
        }
        public async Task<ContentTypeDTO> GetByIdWithContentSection(int id)
        {
            try
            {
                var cT = await _context.ContentTypes.Include(x => x.ContentSections).FirstOrDefaultAsync(x => x.ID == id);
                return _mapper.Map<ContentTypeDTO>(cT);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetByIdWithContentSection-Repo");
                throw;
            }

        }
        public async Task<ContentTypeDTO> GetByIdWithContentSectionHPOnly(int id)
        {
            try
            {
                var cT = await _context.ContentTypes.Include(x => x.ContentSections.Where(x => x.ViewInHP == true)).FirstOrDefaultAsync(x => x.ID == id);
                return _mapper.Map<ContentTypeDTO>(cT);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetByIdWithContentSectionHPOnly-Repo");
                throw;
            }
        }

        public async Task<ContentTypeDTO> GetById(int id)
        {
            try
            {
                var cT = await _context.ContentTypes.FindAsync(id);
                return _mapper.Map<ContentTypeDTO>(cT);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func GetById-Repo");
                throw;
            }
        }
        public async Task<ContentTypeDTO> Create(ContentTypeDTO contentType)
        {
            try
            {
                var ct = await _context.ContentTypes.AddAsync(_mapper.Map<ContentType>(contentType));
                await _context.SaveChangesAsync();
                return contentType;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func Create-Repo");
                throw;
            }
        }

        public async Task Update(ContentTypeDTO contentType)
        {
            try
            {
                _context.ContentTypes.Update(_mapper.Map<ContentType>(contentType));
                // _context.Entry(contentType).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func Update-Repo");
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                ContentType contentType = await _context.ContentTypes.FindAsync(id);
                _context.ContentTypes.Remove(contentType);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "- this error in the func Delete-Repo");
                throw;
            }

        }
    }
}
