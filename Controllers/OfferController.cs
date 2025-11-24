using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TalentoLocal.DTOs;
using TalentoLocal.Services.Implementations;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Controllers
{
    [ApiController]
    [Route("api/offers")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _service;

        public OfferController(IOfferService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var offers = await _service.GetAllAsync();
            return Ok(offers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var offer = await _service.GetByIdAsync(id);
                return Ok(offer);
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
                return StatusCode(500, new { message = "Ocurri� un error al obtener la oferta." });
            }
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OfferDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "El cuerpo de la solicitud no puede estar vac�o." });

                var created = await _service.CreateAsync(dto);

                return Created("api/Offers", created);
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
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error interno del servidor: {ex.Message}" });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OfferDTO dto)
        {
            try
            {
                var result = await _service.UpdateAsync(id, dto);
                return result
                    ? Ok(new { message = "Oferta actualizada correctamente." })
                    : NotFound(new { message = "No se pudo actualizar la oferta." });
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
                return StatusCode(500, new { message = "Ocurri� un error al actualizar la oferta." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                return result
                    ? Ok(new { message = "Oferta eliminada correctamente." })
                    : NotFound(new { message = "No se pudo eliminar la oferta." });
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
                return StatusCode(500, new { message = "Ocurri� un error al eliminar la oferta." });
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOffersByUserId(string userId)
        {
            var offers = await _service.GetOffersByUserIdAsync(userId);

            if (offers == null || !offers.Any())
                return NotFound($"No hay ofertas disponibles para el usuario con ID {userId}");

            return Ok(offers);
        }

        [HttpGet("search-by-category")]
        public async Task<IActionResult> SearchByCategory([FromQuery] string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return BadRequest(new { message = "El campo 'category' es obligatorio." });

            var results = await _service.GetByCategoryAsync(category);

            if (results == null || results.Count == 0)
                return NotFound(new { message = $"No se encontraron ofertas para la categoría '{category}'." });

            return Ok(results);
        }

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetOffersByCompanyId(string companyId)
        {
            var offers = await _service.GetByCompanyIdAsync(companyId);

            if (offers == null || !offers.Any())
                return NotFound($"No hay ofertas disponibles para la empresa con ID {companyId}");

            return Ok(offers);
        }

        [HttpGet("{offerId}/postulation-stats")]
        public async Task<IActionResult> GetPostulationStats(int offerId)
        {
            try
            {
                // Validación 1: obtener la oferta (puede lanzar ArgumentException o KeyNotFoundException)
                var offer = await _service.GetByIdAsync(offerId);

                // Validación 2: obtener estadísticas
                var stats = await _service.GetPostulationStats(offerId);

                return Ok(stats);
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
                return StatusCode(500, new { message = "Ocurrió un error al obtener las estadísticas de la oferta." });
            }
        }

    }
}
