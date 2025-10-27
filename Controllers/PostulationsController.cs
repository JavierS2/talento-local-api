using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;
using TalentoLocal.Models.enums;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostulationsController : ControllerBase
    {
        private readonly IPostulationService _postulationService;

        public PostulationsController(IPostulationService postulationService)
        {
            _postulationService = postulationService;
        }

        [HttpPost("Mockup")]
        public async Task<ActionResult<int>> CreateMockup()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Postulation postu = new Postulation
            {
                ConvocationId = 1,
                ApplicationDate = DateTime.Now,
                Status = PostulationStatus.Pending,
                AttachedDocument = "1234",
                CompanyObservation = "test",
                ReviewDate = DateTime.Now,
                ActionHistory = "Test",
                UserId = 1,
            };

            var postulationId = await _postulationService.AddPostulationAsync(postu);
            return CreatedAtAction(nameof(GetById), new { id = postulationId }, new { id = postulationId });
        }


        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Postulation postulation)
        {
            if (postulation == null) return BadRequest("Body is null");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var postulationId = await _postulationService.AddPostulationAsync(postulation);
            return CreatedAtAction(nameof(GetById), new { id = postulationId }, new { id = postulationId });
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var postulation = await _postulationService.GetAllAsync();
            return Ok(postulation);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Postulation>> GetById(int id)
        {
            var result = await _postulationService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Postulation>> Updated(int id, Postulation postulation)
        {
            if (postulation == null) return BadRequest("Body is null");

            var updated = await _postulationService.UpdateAsync(id, postulation);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _postulationService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
