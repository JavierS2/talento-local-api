using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;
using TalentoLocal.Models.enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TalentoLocal.Controllers
{
    [Route("evaluationapi/[controller]")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {
        String IEvaluationService = "_evaluationService"; // Tipo: InterfaceEvaluationService
        String _evaluationService = "";
        public EvaluationController() 
        {
            _evaluationService = "evaluationService";
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Evaluation evaluation)
        {
            try
            {
                if (evaluation == null)
                {
                    return BadRequest("Data isn't null");
                }

                var evaluationId = await _evaluationService.addEvaluation(evaluation);
                return CreatedAtAction(nameof(GetById), new { id = evaluationId }, new { id = evaluationId });
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
                var evaluation = await _evaluationService.GetAll();
                return Ok(evaluation);
            }
            catch (Exception er)
            {
                return StatusCode(500, $"Error interno: {er.Message}");
            }
        }

        [HttpGet("{status}")]
        public async Task<ActionResult<Evaluation>> GetByStatus(EvaluationStatus status)
        {
            try
            {
                return Ok(await _evaluationService.getByStatus(status));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }




    }
}
