using Microsoft.AspNetCore.Mvc;
using TalentoLocal.DTOs;
using TalentoLocal.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace TalentoLocal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostulationController : ControllerBase
    {
        private readonly IPostulationService _service;

        public PostulationController(IPostulationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var postulaciones = await _service.GetAllAsync();
            return Ok(postulaciones);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var postulation = await _service.GetByIdAsync(id);
                return Ok(postulation);
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
                return StatusCode(500, new { message = "Ocurrió un error al obtener la postulación." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostulationDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("El cuerpo de la solicitud no puede estar vacío.");

                var created = await _service.CreateAsync(dto);

                return Created("api/Postulation", created);
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
        public async Task<IActionResult> Update(int id, [FromBody] PostulationDTO dto)
        {
            try
            {
                var result = await _service.UpdateAsync(id, dto);
                return result
                    ? Ok(new { message = "Postulación actualizada correctamente." })
                    : NotFound(new { message = "No se pudo actualizar la postulación." });
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
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la postulación." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                return result
                    ? Ok(new { message = "Postulación eliminada correctamente." })
                    : NotFound(new { message = "No se pudo eliminar la postulación." });
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
                return StatusCode(500, new { message = "Ocurrió un error al eliminar la postulación." });
            }
        }
    }
}
