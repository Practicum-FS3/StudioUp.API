using StudioUp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.Repo
{
    public interface IContentTypeRepository
    {
        Task<IEnumerable<ContentType>> GetAll();
        Task<ContentType> GetById(int id);
        Task<ContentType> GetByIdWithContentSection(int id);
        Task<ContentType> GetByIdWithContentSectionHPOnly(int id);
        Task<ContentType> Create(ContentType contentType);
        Task<ContentType> Update(ContentType contentType);
        Task Delete(int id);
    }
}
