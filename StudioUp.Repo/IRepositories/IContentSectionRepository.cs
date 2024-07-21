using StudioUp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.Repo
{
    public interface IContentSectionRepository
    {
        Task<IEnumerable<ContentSection>> GetAllAsync();
        Task<ContentSection> GetByIdAsync(int id);
        Task AddAsync(ContentSection contentSection);
        Task UpdateAsync(ContentSection contentSection);
        Task DeleteAsync(ContentSection contentSection);
        Task<IEnumerable<ContentSection>> GetByContentTypeAsync(int contentTypeId); // הוספת פונקציה חדשה
    }
}
