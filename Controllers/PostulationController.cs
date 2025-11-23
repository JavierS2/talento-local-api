using Microsoft.AspNetCore.Mvc;
using TalentoLocal.DTOs;
using TalentoLocal.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace TalentoLocal.Controllers
{
    [ApiController]
    [Route("api/postulations")]
    public class PostulationController : ControllerBase
    {
        private readonly IPostulationService _service;
        private readonly IBlobStorageService _blob;

        public PostulationController(IPostulationService service, IBlobStorageService blob)
        {
            _service = service;
            _blob = blob;
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

        // 🟩 POST: Acepta archivos, se debe usar [FromForm]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PostulationDTO dto)
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

        // 🟩 PUT: También debe aceptar archivos → [FromForm]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] PostulationDTO dto)
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
        [HttpGet("{id}/document")]
        public async Task<IActionResult> GetDocumentUrl(int id)
        {
            // 1. Obtener la postulación
            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
                return NotFound(new { message = "No existe la postulación." });

            // 2. entity.DocumentFile contiene el blobName (ej: "abc123.pdf")

            if (string.IsNullOrWhiteSpace(entity.DocumentFileUrl))
                return BadRequest(new { message = "La postulación no tiene un documento asociado." });

            // 3. Generar SAS URL por 20 min
            string sasUrl = _blob.GenerateSasUrl(entity.DocumentFileUrl);

            return Ok(new { url = sasUrl });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPostulationsByUserId(string userId)
        {
            var postulations = await _service.GetPostulationsByUserIdAsync(userId);

            if (postulations == null || !postulations.Any())
                return NotFound($"No existen postulaciones para el usuario con ID: {userId}");

            return Ok(postulations);
        }




    }
}
