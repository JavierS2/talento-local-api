using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Controllers
{
    [Route("postulationapi/[controller]")]
    [ApiController]
    public class PostulationController : ControllerBase
    {
        private readonly IPostulationService _postulationService;

        public PostulationController(IPostulationService postulationService)
        {
            _postulationService = postulationService;
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
    }
}
