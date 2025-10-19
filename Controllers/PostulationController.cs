using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;

namespace TalentoLocal.Controllers
{
    [Route("postulationapi/[controller]")]
    [ApiController]
    public class PostulationController : ControllerBase
    {
        String _postulationService = "";

        public PostulationController()
        {
            _postulationService = "postulationService";
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Postulacion postulation)
        {
            try
            {
                if (postulation == null)
                {
                    return BadRequest("Data isn't null");
                }

                var postulationId = await _postulationService.addPostulation(postulation);
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
                var postulation = await _postulationService.GetAll();
                return Ok(postulation);

            } catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Postulacion>> GetById(int id)
        {
            try
            {
                return Ok(await _postulationService.GetById(id));

            } catch (Exception ex)
            {
                Console.WriteLine("Error get postulation:", ex.Message);
                return null;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Postulation>> Updated(int id, Postulation postulation)
        {
            if (postulation == null)
            {
                return BadRequest("Data isn't null");

            }
            try
            {
                bool updated = await _postulationService.Updated(id, postulation);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating: ", ex.Message);
                return null;
            }
        }
    }
}
