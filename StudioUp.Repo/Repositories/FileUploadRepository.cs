using AutoMapper;
using Microsoft.AspNetCore.Http;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{
    public class FileUploadRepository : IFileUploadRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public FileUploadRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<FileDownloadDTO> GetFileAsync(int id)
        {
            try
            {
                var file = await _context.Files.FindAsync(id);
                if (file == null || !file.IsActive)
                    return null;
                return _mapper.Map<FileDownloadDTO>(file);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<int> AddFileAsync(IFormFile file)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var fileUpload = new FileUpload
                    {
                        FileName = file.FileName,
                        Data = memoryStream.ToArray(),
                        ContentType = file.ContentType,
                        IsActive = true
                    };
                    _context.Files.Add(fileUpload);
                    await _context.SaveChangesAsync();
                    //return _mapper.Map<FileDownloadDTO>(fileUpload);
                    return fileUpload.Id;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task DeleteFileAsync(int id)
        {
            try
            {
                var fileUpload = await _context.Files.FindAsync(id);
                fileUpload.IsActive = false;
                if (fileUpload == null)
                    throw new Exception("File not found by id");
                // _context.Files.Update(fileUpload);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
