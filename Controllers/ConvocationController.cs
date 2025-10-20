using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TalentoLocal.Controllers
{
    [Route("convocationapi/[controller]")]
    [ApiController]
    public class ConvocationController : ControllerBase
    {
        private readonly IConvocationService _convocationService;

        public ConvocationController(IConvocationService convocationService)
        {
            _convocationService = convocationService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateConvocation([FromBody] Convocation convocation)
        {
            if (convocation == null) return BadRequest("Body is null");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var convocationId = await _convocationService.AddConvocationAsync(convocation);
            return CreatedAtAction(nameof(GetById), new { id = convocationId }, new { id = convocationId });
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var convocation = await _convocationService.GetAllAsync();
            return Ok(convocation);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Convocation>> GetById(int id)
        {
            var conv = await _convocationService.GetByIdAsync(id);
            if (conv == null) return NotFound();
            return Ok(conv);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Convocation>> Updated(int id, Convocation convocation)
        {
            if (convocation == null) return BadRequest("Body is null");

            var updated = await _convocationService.UpdateAsync(id, convocation);
            if (updated == null) return NotFound($"Convocatoria con id {id} no encontrada.");
            return Ok(updated);
        }

        [HttpGet("location")]
        public async Task<ActionResult<List<Convocation>>> SearchByLocation([FromQuery] string location)
        {
            var convocation = await _convocationService.SearchByLocationAsync(location);
            return Ok(convocation);
        }

        [HttpGet("availableplaces")]
        public async Task<ActionResult<List<Convocation>>> SearchByAvailablePlaces([FromQuery] int minAvailablePlaces)
        {
            var convocation = await _convocationService.SearchByAvailablePlacesAsync(minAvailablePlaces);
            return Ok(convocation);
        }
    }
}
