using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudioUp.DTO;
using StudioUp.Repo.IRepositories;
using System.Linq;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadRepository _fileUploadRepository;

        public FileUploadController(IFileUploadRepository fileUploadRepository)
        {
            _fileUploadRepository = fileUploadRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
            var fileDownload = await _fileUploadRepository.GetFileAsync(id);
            if (fileDownload == null)
                return NotFound("File not found ");
            return File(fileDownload.Data, fileDownload.ContentType, fileDownload.FileName);
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadDTO fileUploadDTO)
        {
            if (fileUploadDTO.File == null || fileUploadDTO.File.Length == 0)
                return BadRequest("File not selected");

            var permittedExtensions = new[] { ".pdf", ".png", ".jpg" };
            var extension = Path.GetExtension(fileUploadDTO.File.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                return BadRequest("Invalid file type");
            const long maxFileSize = 2 * 1024 * 1024;
            if (fileUploadDTO.File.Length > maxFileSize)
                return BadRequest("File size exceeds the limit of 2 MB");
            var result = await _fileUploadRepository.AddFileAsync(fileUploadDTO.File);
            return Ok(new { id = result });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var result = await _fileUploadRepository.DeleteFileAsync(id);
            if (!result)
                return NotFound("File not found ");
            return Ok(new { Message = "File deleted successfully" });
        }
    }
}
