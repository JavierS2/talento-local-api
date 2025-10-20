using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;
using TalentoLocal.Models.enums;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {
        private readonly IEvaluationService _evaluationService;

        public EvaluationController(IEvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
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
    }
}
