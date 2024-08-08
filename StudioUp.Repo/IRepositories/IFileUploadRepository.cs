using Microsoft.AspNetCore.Http;
using StudioUp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface IFileUploadRepository
    {
        Task<FileDownloadDTO> GetFileAsync(int id);
        Task<FileDownloadDTO> AddFileAsync(IFormFile file);
        Task DeleteFileAsync(int id);
    }
}
