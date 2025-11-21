using Microsoft.AspNetCore.Mvc;
using TalentoLocal.DTOs;
using TalentoLocal.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace TalentoLocal.Controllers
{
    [ApiController]
    [Route("api/postulations-status")]
    public class PostulationStatusController : ControllerBase
    {
        private readonly IPostulationStatusService _service;

        public PostulationStatusController(IPostulationStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var statuses = await _service.GetAllAsync();
            return Ok(statuses);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var status = await _service.GetByIdAsync(id);
                return Ok(status);
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
                return StatusCode(500, new { message = "Ocurrió un error al obtener el estado de postulación." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostulationStatusDTO dto)
        {
            try
            {

                if (dto == null)
                    return BadRequest(new { message = "El cuerpo de la solicitud no puede estar vacío." });

                if (string.IsNullOrWhiteSpace(dto.Name))
                    return BadRequest(new { message = "El nombre del estado de postulación es obligatorio." });

                var created = await _service.CreateAsync(dto);

                // Retornar 201 Created
                return Created("api/PostulationStatus", created);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al crear el estado de postulación." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PostulationStatusDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "El cuerpo de la solicitud no puede estar vacío." });

                if (string.IsNullOrWhiteSpace(dto.Name))
                    return BadRequest(new { message = "El nombre del estado de postulación es obligatorio." });

                var result = await _service.UpdateAsync(id, dto);

                return result
                    ? Ok(new { message = "Estado de postulación actualizado correctamente." })
                    : NotFound(new { message = "No se pudo actualizar el estado de postulación." });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
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
                return StatusCode(500, new { message = "Ocurrió un error al actualizar el estado de postulación." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);

                return result
                    ? Ok(new { message = "Estado de postulación eliminado correctamente." })
                    : NotFound(new { message = "No se pudo eliminar el estado de postulación." });
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
                return StatusCode(500, new { message = "Ocurrió un error al eliminar el estado de postulación." });
            }
        }
    }
}
