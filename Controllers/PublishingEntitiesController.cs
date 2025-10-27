using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishingEntitiesController : ControllerBase
    {
        private readonly IPublishingEntityService _publishingEntityService;

        public PublishingEntitiesController(IPublishingEntityService publishingEntityService)
        {
            _publishingEntityService = publishingEntityService;
        }

        [HttpPost("Mockup")]
        public async Task<ActionResult<int>> CreateMockup()
        {
            try
            {

                PublishingEntity publ = new PublishingEntity
                {
                    Name = "Homecenter",
                    Type = Models.enums.EntityType.Company,
                    Description = "Estableimiento especializado en ofrecer una amplia variedad de productos para la mejora del hogar y la construccion",
                    Email = "homecenter@gmail.com",
                    Phone = " 300 123 23 22",
                    SiteWeb = "www.homecenter.com.co",
                    Address = "Carrera 35 # 29a-355, Santa Marta, Magdalena 470000",
                };

                var postulationId = await _publishingEntityService.AddPublishingEntityAsync(publ);
                return CreatedAtAction(nameof(GetById), new { id = postulationId }, new { id = postulationId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _publishingEntityService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
