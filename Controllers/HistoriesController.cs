using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriesController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        public HistoriesController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] History history)
        {
            if (history == null) return BadRequest("Body is null");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var historyId = await _historyService.AddHistoryAsync(history);
            return CreatedAtAction(nameof(GetById), new { id = historyId }, new { id = historyId });
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var history = await _historyService.GetAllAsync();
            return Ok(history);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<History>> GetById(int id)
        {
            var result = await _historyService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<History>> Update(int id, History history)
        {
            if (history == null) return BadRequest("Body is null");

            var updated = await _historyService.UpdateAsync(id, history);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _historyService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
