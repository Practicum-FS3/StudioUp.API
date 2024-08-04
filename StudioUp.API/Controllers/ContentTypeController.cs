using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Repo;
using AutoMapper;


namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentTypeController : ControllerBase
    {
        private readonly IContentTypeRepository _repository;
        private readonly ILogger<ContentTypeController> _logger;

        public ContentTypeController(IContentTypeRepository repository,ILogger<ContentTypeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentTypeDTO>>> GetAll()
        {
            try
            {
                var contentTypes = await _repository.GetAll();
                return Ok(contentTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentTypeController/GetAll");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetByIdWithContentSection/{id}")]
        public async Task<ActionResult<ContentTypeDTO>> GetByIdWithContentSection(int id)
        {
            try
            {
                ContentTypeDTO contentTypes = await _repository.GetByIdWithContentSection(id);
                if (contentTypes == null)
                {
                    return NotFound("content types not found by ID");
                }
                return Ok(contentTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentTypeController/GetByIdWithContentSection");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpGet("GetByIdWithContentSectionHPOnly/{id}")]
        public async Task<ActionResult<ContentTypeDTO>> GetByIdWithContentSectionHPOnly(int id)
        {
            try
            {
                ContentTypeDTO contentTypes = await _repository.GetByIdWithContentSectionHPOnly(id);
                if (contentTypes == null)
                {
                    return NotFound("content types not found by ID");

                }
                return Ok(contentTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentTypeController/GetByIdWithContentSectionHPOnly");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ContentTypeDTO>> GetById(int id)
        {
            try
            {
                var contentType = await _repository.GetById(id);
                if (contentType == null)
                {
                    return NotFound("content types not found by ID");
                }

                return Ok(contentType);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, " this error in ContentTypeController/GetById");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost]
        public async Task<ActionResult<ContentTypeDTO>> Create(ContentTypeDTO contentTypeDTO)
        {
            if (contentTypeDTO == null)
            {
                return BadRequest("The content type field is null.");
            }
            try
            {
                var contentType = await _repository.Create(contentTypeDTO);
                return Ok(contentType); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentTypeController/Create");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ContentTypeDTO contentTypeDTO)
        {
            if (contentTypeDTO == null)
            {
                return BadRequest("The content type field is null.");
            }
            if (id != contentTypeDTO.ID)
            {
                _logger.LogError("cant update in ContentTypeController/Update");
                return BadRequest();
            }
            try
            {
                await _repository.Update(contentTypeDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentTypeController/Update");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentTypeController/Delete");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}
