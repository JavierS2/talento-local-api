using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishingEntityController : ControllerBase
    {
        private readonly IPublishingEntityService _publishingEntityService;

        public PublishingEntityController(IPublishingEntityService publishingEntityService)
        {
            _publishingEntityService = publishingEntityService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] PublishingEntity publishingEntity)
        {
            try
            {
                if (publishingEntity == null)
                {
                    return BadRequest("Data isn't null");
                }

                var postulationId = await _publishingEntityService.AddPublishingEntityAsync(publishingEntity);
                return CreatedAtAction(nameof(GetById), new { id = postulationId }, new { id = postulationId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var PublishingEntity = await _publishingEntityService.GetAllAsync();
                return Ok(PublishingEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublishingEntity>> GetById(int id)
        {
            try
            {
                var entity = await _publishingEntityService.GetByIdAsync(id);
                if (entity == null) return NotFound();
                return Ok(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PublishingEntity>> Updated(int id, PublishingEntity publishing)
        {
            if (publishing == null)
            {
                return BadRequest("Data isn't null");
            }

            try
            {
                bool updated = await _publishingEntityService.UpdateAsync(id, publishing);
                if (!updated) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating: ", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
