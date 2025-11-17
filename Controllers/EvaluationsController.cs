using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationsController : ControllerBase
    {
        private readonly IEvaluationService _evaluationService;

        public EvaluationsController(IEvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
        }
        [HttpPost("Mockup")]
        public async Task<ActionResult<Evaluation>> CreateMockup()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Evaluation eva = new()
            {
                PostulationId = 1,
                Justification = "Conocimientos técnicos, comunicación y resolución de problemas.",
                
            };

            var evaluation = await _evaluationService.CreateAsync(eva);
            return CreatedAtAction(nameof(GetById), new { id = evaluation.Id}, evaluation);
        }

        [HttpPost]
        public async Task<ActionResult<Evaluation>> Create([FromBody] Evaluation evaluation)
        {
            try
            {
                if (evaluation == null) return NotFound(new {message = "Failed to create the evaluation (Data is null)"});
                if (!ModelState.IsValid) return NotFound(new { message = "Failed to create the evaluation" });

                var eva = await _evaluationService.CreateAsync(evaluation);
                return CreatedAtAction(nameof(GetById), new { id = eva.Id }, eva);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {message = "Error creating evaluation", error = ex.Message});
            }
            
        }

        [HttpGet]
        public async Task<ActionResult<List<Evaluation>>> GetAll()
        {
            try
            {
                var evaluation = await _evaluationService.GetAllAsync();
                return (evaluation != null) ? Ok(evaluation) : NotFound(new { message = "Failed to fetch the evaluations" });
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching evaluations", error = ex.Message });
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Evaluation>> GetById(int id)
        {
            try
            {
                var ev = await _evaluationService.GetByIdAsync(id);
                return (ev != null) ? Ok(ev) : NotFound(new { message = "Failed to fetch the evaluation" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching evaluation", error = ex.Message });
            }
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<Evaluation>> Updated(int id,[FromBody] Evaluation eva)
        {
            try
            {
                if(id < 0 || eva  == null) return NotFound( new { message = "Failed to update the evaluation (NOT FOUND)"});
                var ev = await _evaluationService.UpdateAsync(id, eva);
                return ev ? Ok(ev) : NotFound(new { message = "Failed to update the evaluation" });
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error applying evaluation update", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 0) return NotFound(new { message = "Failed to delete the evaluation (NOT FOUND)" });
                var deleted = await _evaluationService.DeleteAsync(id);
                return deleted ? Ok(deleted) : NotFound(new { message = "Failed to delete the evaluation" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting evaluation", error = ex.Message });
            }
        }
    }
}
