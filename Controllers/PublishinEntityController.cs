using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;

namespace TalentoLocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishinEntityController : ControllerBase
    {
        String _publishingEntityService = "";

        public PublishinEntityController()
        {
            _publishingEntityService = "publishingEntityService";
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

                var postulationId = await _publishingEntityService.addPublishingEntity(publishingEntity);
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
                var PublishingEntity = await _publishingEntityService.getAll();
                return Ok(PublishingEntity);
            } catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublishingEntity>> GetById(int id)
        {
            try
            {
                return Ok(await _publishingEntityService.GetById(id));
            } catch (Exception ex)
            {
                Console.WriteLine("Error:", ex.Message);
                return null;
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
                bool updated = await _publishingEntityService.Updated(id, publishing);
            }catch (Exception ex)
            {
                Console.WriteLine("Error updating: ", ex.Message);
                return null;
            }
        }
    }
}
