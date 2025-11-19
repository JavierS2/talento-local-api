using Microsoft.AspNetCore.Mvc;
using TalentoLocal.DTOs;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Controllers
{
    [ApiController]
    [Route("api/favorites")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _service;

        public FavoriteController(IFavoriteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var favorites = await _service.GetAllAsync();
            return Ok(favorites);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var favorite = await _service.GetByIdAsync(id);
                return Ok(favorite);
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
                return StatusCode(500, new { message = "Ocurrió un error al obtener el favorito." });
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            try
            {
                var favorites = await _service.GetByUserAsync(userId);
                return Ok(favorites);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al obtener los favoritos del usuario." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FavoriteDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("El cuerpo de la solicitud no puede estar vacío.");

                var created = await _service.CreateAsync(dto);
                // coherente con [Route("api/favorites")]
                return Created("api/favorites", created);
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
        public async Task<IActionResult> Update(int id, [FromBody] FavoriteDTO dto)
        {
            try
            {
                var result = await _service.UpdateAsync(id, dto);
                return result
                    ? Ok(new { message = "Favorito actualizado correctamente." })
                    : NotFound(new { message = "No se pudo actualizar el favorito." });
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
                return StatusCode(500, new { message = "Ocurrió un error al actualizar el favorito." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                return result
                    ? Ok(new { message = "Favorito eliminado correctamente." })
                    : NotFound(new { message = "No se pudo eliminar el favorito." });
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
                return StatusCode(500, new { message = "Ocurrió un error al eliminar el favorito." });
            }
        }

        public record FavoriteToggleRequest(int UserId, int OfferId);

        [HttpPost("toggle")]
        public async Task<IActionResult> Toggle([FromBody] FavoriteToggleRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest("El cuerpo de la solicitud no puede estar vacío.");

                var isFavorite = await _service.ToggleAsync(request.UserId, request.OfferId);

                return Ok(new
                {
                    message = isFavorite
                        ? "La oferta fue agregada a favoritos."
                        : "La oferta fue eliminada de favoritos.",
                    isFavorite
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al realizar el toggle de favorito." });
            }
        }
    }
}
