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
        private readonly ILogger<FileUploadController> _logger;

        public FileUploadController(IFileUploadRepository fileUploadRepository, ILogger<FileUploadController> logger)
        {
            _fileUploadRepository = fileUploadRepository;
            _logger = logger;
        }

        [HttpGet("GetFileById/{id}")]
        public async Task<IActionResult> GetFileById(int id)
        {
            try
            {
                var fileDownload = await _fileUploadRepository.GetFileAsync(id);
                if (fileDownload == null)
                    return NotFound("File not found ");
                return File(fileDownload.Data, fileDownload.ContentType, fileDownload.FileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in FileUploadController/GetFileById");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpPost("UploadFile")]
        public async Task<ActionResult> UploadFile([FromForm] FileUploadDTO fileUploadDTO)
        {
            try
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
                return File(result.Data, result.ContentType, result.FileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in FileUploadController/UploadFile");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpDelete("DeleteFile/{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            try
            {
                await _fileUploadRepository.DeleteFileAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in FileUploadController/DeleteFile");
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }


    }
}
