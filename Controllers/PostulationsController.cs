using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostulationsController(IPostulationService postulationService) : ControllerBase
    {
        private readonly IPostulationService _postulationService = postulationService;

        [HttpPost("Mockup")]
        public async Task<ActionResult<int>> CreateMockup()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Postulation postu = new()
            {
                OfferId = 1,
                DocumentFile = 1005895684,
                StatusId = 1,
                UserId = 1,
            };

            var pos = await _postulationService.CreateAsync(postu);
            return CreatedAtAction(nameof(GetById), new { id = pos.Id }, new { id = pos });
        }


        [HttpPost]
        public async Task<ActionResult<PostulationStatus>> Create([FromBody] Postulation postulation)
        {
            try
            {
                if (postulation == null) return NotFound(new {message = "Failed to create the postulation (Data is null)"});
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var postu = await _postulationService.CreateAsync(postulation);
                return CreatedAtAction(nameof(GetById), new { id = postu.Id }, new { id = postu });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating postulation", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var postulation = await _postulationService.GetAllAsync();
                if (postulation == null) return NotFound(new { message = "Failed to fetch the postulations (NOT FOUND) " });
                return Ok(postulation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching postulations", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Postulation>> GetById(int id)
        {
            try
            {
                var result = await _postulationService.GetByIdAsync(id);
                if (result == null) return NotFound(new {message = "Failed to fetch the postulation (NOT FOUND)"});
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching postulation", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Postulation>> Updated(int id,[FromBody] Postulation postulation)
        {
            try
            {
                if (postulation == null || id < 0) return NotFound(new {message = "Failed to update the postulation (NOT FOUND)"});

                var updated = await _postulationService.UpdateAsync(id, postulation);

                return updated ? Ok(updated) : NotFound(new { message = "Failed to update the postulation" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating postulation", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 0) return NotFound(new { message = "Failed to delete the postulation (NOT FOUND)" });
                var deleted = await _postulationService.DeleteAsync(id);
                return deleted ? Ok(deleted) : NotFound(new { message = "Failed to delete the postulation" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting postulation", error = ex.Message });
            }
        }
    }
}
