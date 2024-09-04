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
        [HttpPost("UploadFileAndReturnFile")]
        public async Task<ActionResult> UploadFileAndReturnFile([FromForm] FileUploadDTO fileUploadDTO)
        {
            try
            {
                var validationResult = ValidateFile(fileUploadDTO.File);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.ErrorMessage);

                var fileId = await SaveFileAsync(fileUploadDTO.File);

                // Fetch the uploaded file data to return
                var fileResult = await _fileUploadRepository.GetFileAsync(fileId);
                if (fileResult == null)
                    return NotFound("File not found.");

                return File(fileResult.Data, fileResult.ContentType, fileResult.FileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in FileUploadController/UploadFileAndReturnFile");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/fileupload/uploadfileandreturnid
        [HttpPost("UploadFileAndReturnId")]
        public async Task<ActionResult> UploadFileAndReturnId([FromForm] FileUploadDTO fileUploadDTO)
        {
            try
            {
                var validationResult = ValidateFile(fileUploadDTO.File);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.ErrorMessage);

                var fileId = await SaveFileAsync(fileUploadDTO.File);
                return Ok(new { id = fileId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in FileUploadController/UploadFileAndReturnId");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Internal method to validate the file
        private (bool IsValid, string ErrorMessage) ValidateFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return (false, "File not selected");

            var permittedExtensions = new[] { ".pdf", ".png", ".jpg" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                return (false, "Invalid file type");

            const long maxFileSize = 2 * 1024 * 1024;
            if (file.Length > maxFileSize)
                return (false, "File size exceeds the limit of 2 MB");

            return (true, null);
        }

        // Internal method to save the file
        private async Task<int> SaveFileAsync(IFormFile file)
        {
            // Call the repository or service to save the file and get the ID
            return await _fileUploadRepository.AddFileAsync(file);
        }


        //[HttpPost("UploadFile")]
        //public async Task<ActionResult> UploadFile([FromForm] FileUploadDTO fileUploadDTO)
        //{
        //    try
        //    {
        //        if (fileUploadDTO.File == null || fileUploadDTO.File.Length == 0)
        //            return BadRequest("File not selected");

        //        var permittedExtensions = new[] { ".pdf", ".png", ".jpg" };
        //        var extension = Path.GetExtension(fileUploadDTO.File.FileName).ToLowerInvariant();
        //        if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
        //            return BadRequest("Invalid file type");
        //        const long maxFileSize = 2 * 1024 * 1024;
        //        if (fileUploadDTO.File.Length > maxFileSize)
        //            return BadRequest("File size exceeds the limit of 2 MB");
        //        var result = await _fileUploadRepository.AddFileAsync(fileUploadDTO.File);
        //        //return File(result.Data, result.ContentType, result.FileName);
        //        return Ok(new { id = result });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, " this error in FileUploadController/UploadFile");
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }

        //}
        [HttpDelete("DeleteFile/{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var result = await _fileUploadRepository.DeleteFileAsync(id);
            if (!result)
                return NotFound("File not found ");
            return Ok(new { Message = "File deleted successfully" });
        }
    }
}
