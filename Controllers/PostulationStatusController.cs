using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TalentoLocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostulationStatusController(IPostulationStatusService postulationStatusService) : ControllerBase
    {
        private readonly IPostulationStatusService _postulationStatusService = postulationStatusService;

        [HttpPost("Mockup")]
        public async Task<ActionResult<int>> CreateMockup()
        {
            PostulationStatus pos = new()
            {
                Name = "Abierto",

            };

            var createPos = await _postulationStatusService.CreateAsync(pos);

            return CreatedAtAction(nameof(GetById), new { id = createPos.Id }, createPos);
        }

        [HttpGet]
        public async Task<ActionResult<List<PostulationStatus>>> GetAll()
        {
            try
            {
                var pos = await _postulationStatusService.GetAllAsync();
                if(pos == null)
                {
                    return NotFound(new {message = "Failed fetching the postulation status"});
                }
                return Ok(pos);
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error obtaining the postulations status", error = ex.Message });
            }
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<PostulationStatus>> GetById(int id)
        {
            try
            {
            var pos = await _postulationStatusService.GetByIdAsync(id);
            if (pos == null)
            {
                return NotFound( new { message = "Failed fetching postulation status (Postulation status ID NOT FOUND)"});
            }
            return Ok(pos);

            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error obtaining the postulation status ID", error = ex.Message });
            }
        }

        
        [HttpPost]
        public async Task<ActionResult<PostulationStatus>> Create([FromBody] PostulationStatus postulation)
        {
            try
            {
                if (postulation == null) return NotFound( new { message = "Failed creating postulation status" });
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var pos = await _postulationStatusService.CreateAsync(postulation);

                return CreatedAtAction(nameof(GetById), new { id = pos.Id }, pos);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating postulation status", error = ex.Message });
            }
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<PostulationStatus>> Update(int id, [FromBody] PostulationStatus value)
        {
            try
            {
                if (value == null || id < 0) return NotFound(new { message = "Failed updating postulation status (Postulation status NOT FOUND)" });
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var pos = await _postulationStatusService.UpdateAsync(id, value);

                return Ok(pos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating postulation status", error = ex.Message });
            }

        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<PostulationStatus>> Delete(int id)
        {
            try
            {
                var pos = await _postulationStatusService.DeleteAsync(id);
                return pos ? Ok(id) : NotFound(new { message = "Failed deleting" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting postulation status ID", error = ex.Message });
            }
        }
    }
}
