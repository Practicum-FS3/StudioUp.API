using StudioUp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StudioUp.Repo
{
    public class ContentSectionRepository : IContentSectionRepository
    {
        private readonly DataContext _context;

        public ContentSectionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContentSection>> GetAllAsync()
        {
            return await _context.ContentSections.Include(cs => cs.ContentType).ToListAsync();
        }

        public async Task<ContentSection> GetByIdAsync(int id)
        {
            return await _context.ContentSections.Include(cs => cs.ContentType).FirstOrDefaultAsync(cs => cs.ID == id);
        }

        public async Task AddAsync(ContentSection contentSection)
        {
            await _context.ContentSections.AddAsync(contentSection);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ContentSection contentSection)
        {
            _context.ContentSections.Update(contentSection);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ContentSection contentSection)
        {
            _context.ContentSections.Remove(contentSection);
            await _context.SaveChangesAsync();
        }
    }
}
