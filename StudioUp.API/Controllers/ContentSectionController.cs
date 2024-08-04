using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentSectionController : ControllerBase
    {
        private readonly IContentSectionRepository _repository;
        private readonly ILogger<ContentSectionController> _logger;


        public ContentSectionController(IContentSectionRepository repository, ILogger<ContentSectionController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentSectionDTO>>> GetContentSections()
        {
            try
            {
                var contentSections = await _repository.GetAllAsync();
                return Ok(contentSections);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentSectionController/GetContentSections");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContentSectionDTO>> GetContentSection(int id)
        {
            try
            {
                var contentSection = await _repository.GetByIdAsync(id);
                if (contentSection == null)
                {
                    return NotFound("content section not found by ID");
                }
                return Ok(contentSection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentSectionController/GetContentSection");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("byContentType/{contentTypeId}")]
        public async Task<ActionResult<IEnumerable<ContentSectionDTO>>> GetContentSectionsByContentType(int contentTypeId)
        {
            try
            {
                var contentSections = await _repository.GetByContentTypeAsync(contentTypeId);
                if (contentSections == null)
                {
                    return NotFound("content section not found by content type");
                }
                return Ok(contentSections);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, " this error in ContentSectionController/GetContentSectionsByContentType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost]
        public async Task<ActionResult<ContentSectionDTO>> CreateContentSection(ContentSectionDTO contentSectionDTO)
        {
            if (contentSectionDTO == null)
            {
                return BadRequest("The content section field is null.");
            }
            try
            {
                var contentSection = await _repository.AddAsync(contentSectionDTO);
                return Ok(contentSection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentSectionController/CreateContentSection");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContentSection(int id, ContentSectionDTO contentSectionDTO)
        {
            if (contentSectionDTO == null)
            {
                return BadRequest("The content section field is null.");
            }
            if (id != contentSectionDTO.ID)
            {
                return BadRequest("ID in URL does not match ID in body");
            }
            try
            {
                var contentSection = await _repository.GetByIdAsync(id);
                await _repository.UpdateAsync(contentSection);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentSectionController/UpdateContentSection");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContentSection(int id)
        {
            try
            {
                var contentSection = await _repository.GetByIdAsync(id);
                if (contentSection == null)
                {
                    return NotFound("content section not found by ID");
                }
                await _repository.DeleteAsync(contentSection);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentSectionController/DeleteContentSection");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}
