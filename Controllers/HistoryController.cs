using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;

namespace TalentoLocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        String _historyService = "";

        public HistoryController()
        {
            _historyService = "historyService";
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] History history)
        {
            try
            {
                if (history == null)
                {
                    return BadRequest("Data isn't null");
                }

                var historyId = await _historyService.addHistory(history);
                return CreatedAtAction(nameof(GetById), new { id = historyId }, new { id = historyId });
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
                var history = await _historyService.GetAll();
                return Ok(history);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<History>> GetById(int id)
        {
            try
            {
                return Ok(await _historyService.GetById(id));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error get postulation:", ex.Message);
                return null;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<History>> Update(int id, History history)
        {
            if (history == null)
            {
                return BadRequest("Data isn't null");

            }
            try
            {
                bool updated = await _historyService.Updated(id, history);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating: ", ex.Message);
                return null;
            }
        }
    }
}
