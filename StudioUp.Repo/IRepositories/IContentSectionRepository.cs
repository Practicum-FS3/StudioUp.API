using StudioUp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.Repo
{
    public interface IContentSectionRepository
    {
        Task<IEnumerable<ContentSectionDowoladDTO>> GetAllAsync();
        Task<ContentSectionDowoladDTO> GetByIdAsync(int id);
        Task<ContentSectionDowoladDTO> AddAsync(ContentSectionUploadDTO contentSection);
        Task UpdateAsync(ContentSectionUploadDTO contentSection);
        Task DeleteAsync(int ID);
        Task<IEnumerable<ContentSectionDowoladDTO>> GetByContentTypeAsync(int contentTypeId); // הוספת פונקציה חדשה
    }
}
