using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;
using TalentoLocal.Models.enums;
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
        public async Task<ActionResult<int>> CreateMockup()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Evaluation eva = new Evaluation
            {
                PostulationId = 1,
                EvaluatorId = 1,
                Criteria = "Conocimientos técnicos, comunicación y resolución de problemas.",
                Result = 4.5f,
                Comments = "El candidato demostró un buen dominio técnico y habilidades comunicativas.",
                EvaluationDate = DateTime.Now,
                Status = EvaluationStatus.Evaluated
            };

            var evaluationId = await _evaluationService.AddEvaluationAsync(eva);
            return CreatedAtAction(nameof(GetByStatus), new { id = evaluationId}, new { id = evaluationId });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Evaluation evaluation)
        {
            if (evaluation == null) return BadRequest("Body is null");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var evaluationId = await _evaluationService.AddEvaluationAsync(evaluation);
            return CreatedAtAction(nameof(GetByStatus), new { status = evaluation.Status }, new { id = evaluationId });
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var evaluation = await _evaluationService.GetAllAsync();
            return Ok(evaluation);
        }

        [HttpGet("{status}")]
        public async Task<ActionResult<Evaluation>> GetByStatus(EvaluationStatus status)
        {
            var ev = await _evaluationService.GetByStatusAsync(status);
            if (ev == null) return NotFound();
            return Ok(ev);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _evaluationService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
