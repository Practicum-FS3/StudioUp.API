using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentTypeController : ControllerBase
    {
        private readonly IContentTypeRepository _repository;
        private readonly IMapper _mapper;

        public ContentTypeController(IContentTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentTypeDTO>>> GetAll()
        {
            var contentTypes = await _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<ContentTypeDTO>>(contentTypes));
        }

        [HttpGet("GetByIdWithContentSection/{id}")]
        public async Task<ActionResult<ContentTypeDTO>> GetByIdWithContentSection(int id)
        {
            ContentType contentTypes = await _repository.GetByIdWithContentSection(id);
            return Ok(_mapper.Map<ContentTypeDTO>(contentTypes));
        }
         [HttpGet("GetByIdWithContentSectionHPOnly/{id}")]
        public async Task<ActionResult<ContentTypeDTO>> GetByIdWithContentSectionHPOnly(int id)
        {
            ContentType contentTypes = await _repository.GetByIdWithContentSectionHPOnly(id);
            return Ok(_mapper.Map<ContentTypeDTO>(contentTypes));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ContentTypeDTO>> GetById(int id)
        {
            var contentType = await _repository.GetById(id);
            if (contentType == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ContentTypeDTO>(contentType));
        }

        [HttpPost]
        public async Task<ActionResult<ContentTypeDTO>> Create(ContentTypeDTO contentTypeDTO)
        {
            var contentType = _mapper.Map<ContentType>(contentTypeDTO);
            contentType = await _repository.Create(contentType);

            return CreatedAtAction(nameof(GetById), new { id = contentType.ID }, _mapper.Map<ContentTypeDTO>(contentType));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ContentTypeDTO>> Update(int id, ContentTypeDTO contentTypeDTO)
        {
            if (id != contentTypeDTO.ID)
            {
                return BadRequest();
            }

            var contentType = _mapper.Map<ContentType>(contentTypeDTO);
            contentType = await _repository.Update(contentType);

            return Ok(_mapper.Map<ContentTypeDTO>(contentType));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);
            return NoContent();
        }
    }
}
