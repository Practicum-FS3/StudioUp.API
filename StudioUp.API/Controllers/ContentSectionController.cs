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
        private readonly IMapper _mapper;

        public ContentSectionController(IContentSectionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentSectionDTO>>> GetContentSections()
        {
            var contentSections = await _repository.GetAllAsync();
            var contentSectionDTOs = _mapper.Map<IEnumerable<ContentSectionDTO>>(contentSections);
            return Ok(contentSectionDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContentSectionDTO>> GetContentSection(int id)
        {
            var contentSection = await _repository.GetByIdAsync(id);

            if (contentSection == null)
            {
                return NotFound();
            }

            var contentSectionDTO = _mapper.Map<ContentSectionDTO>(contentSection);
            return Ok(contentSectionDTO);
        }

        [HttpGet("byContentType/{contentTypeId}")]
        public async Task<ActionResult<IEnumerable<ContentSectionDTO>>> GetContentSectionsByContentType(int contentTypeId)
        {
            var contentSections = await _repository.GetByContentTypeAsync(contentTypeId);
            var contentSectionDTOs = _mapper.Map<IEnumerable<ContentSectionDTO>>(contentSections);
            return Ok(contentSectionDTOs);
        }

        [HttpPost]
        public async Task<ActionResult<ContentSectionDTO>> CreateContentSection(ContentSectionDTO contentSectionDTO)
        {
            var contentSection = _mapper.Map<ContentSection>(contentSectionDTO);
            await _repository.AddAsync(contentSection);
            contentSectionDTO.ID = contentSection.ID;
            return CreatedAtAction(nameof(GetContentSection), new { id = contentSectionDTO.ID }, contentSectionDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContentSection(int id, ContentSectionDTO contentSectionDTO)
        {
            if (id != contentSectionDTO.ID)
            {
                return BadRequest();
            }

            var contentSection = await _repository.GetByIdAsync(id);

            if (contentSection == null)
            {
                return NotFound();
            }

            _mapper.Map(contentSectionDTO, contentSection);
            await _repository.UpdateAsync(contentSection);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContentSection(int id)
        {
            try
            {
                var contentSection = await _repository.GetByIdAsync(id);
                if (contentSection == null)
                {
                    return NotFound($"ContentSection with ID {id} not found.");
                }

                contentSection.IsActive = false;
                await _repository.UpdateAsync(contentSection);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
