using Microsoft.AspNetCore.Mvc;
using TalentoLocal.DTOs;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Controllers
{
    [ApiController]
    [Route("api/evaluations")]
    public class EvaluationController : ControllerBase
    {
        private readonly IEvaluationService _service;

        public EvaluationController(IEvaluationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var evaluations = await _service.GetAllAsync();
            return Ok(evaluations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evaluation = await _service.GetByIdAsync(id);
                return Ok(evaluation);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al obtener la evaluación." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EvaluationDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("El cuerpo de la solicitud no puede estar vacío.");

                var created = await _service.CreateAsync(dto);
                return Created("api/Evaluations", created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EvaluationDTO dto)
        {
            try
            {
                var result = await _service.UpdateAsync(id, dto);
                return result
                    ? Ok(new { message = "Evaluación actualizada correctamente." })
                    : NotFound(new { message = "No se pudo actualizar la evaluación." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la evaluación." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                return result
                    ? Ok(new { message = "Evaluación eliminada correctamente." })
                    : NotFound(new { message = "No se pudo eliminar la evaluación." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al eliminar la evaluación." });
            }
        }
    }
}
