using StudioUp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.Repo
{
    public interface IContentSectionRepository
    {
        Task<IEnumerable<ContentSectionDTO>> GetAllAsync();
        Task<ContentSectionDTO> GetByIdAsync(int id);
        Task<ContentSectionDTO> AddAsync(ContentSectionDTO contentSection);
        Task UpdateAsync(ContentSectionDTO contentSection);
        Task DeleteAsync(ContentSectionDTO contentSection);
        Task<IEnumerable<ContentSectionDTO>> GetByContentTypeAsync(int contentTypeId); // הוספת פונקציה חדשה
    }
}
