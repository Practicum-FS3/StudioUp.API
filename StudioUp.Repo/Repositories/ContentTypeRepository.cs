using StudioUp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StudioUp.Repo
{
    public class ContentTypeRepository : IContentTypeRepository
    {
        private readonly DataContext _context;

        public ContentTypeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContentType>> GetAll()
        {
            return await _context.ContentTypes.ToListAsync();
        }
        public async Task<ContentType> GetByIdWithContentSection(int id)
        {
            return await _context.ContentTypes.Include(x=>x.ContentSections).FirstOrDefaultAsync(x=>x.ID==id);
        }
        public async Task<ContentType> GetByIdWithContentSectionHPOnly(int id)
        {
            return await _context.ContentTypes.Include(x => x.ContentSections.Where(x=>x.ViewInHP==true)).FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<ContentType> GetById(int id)
        {
            return await _context.ContentTypes.FindAsync(id);
        }

        public async Task<ContentType> Create(ContentType contentType)
        {
            _context.ContentTypes.Add(contentType);
            await _context.SaveChangesAsync();
            return contentType;
        }

        public async Task<ContentType> Update(ContentType contentType)
        {
            _context.Entry(contentType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return contentType;
        }

        public async Task Delete(int id)
        {
            var contentType = await _context.ContentTypes.FindAsync(id);
            _context.ContentTypes.Remove(contentType);
            await _context.SaveChangesAsync();
        }
    }
}
