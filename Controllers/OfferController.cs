using Microsoft.AspNetCore.Mvc;
using TalentoLocal.DTOs;
using TalentoLocal.Services.Interfaces;
using System;
using System.Threading.Tasks;

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
                return StatusCode(500, new { message = "Ocurrió un error al obtener la oferta." });
            }
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OfferDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "El cuerpo de la solicitud no puede estar vacío." });

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
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la oferta." });
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
                return StatusCode(500, new { message = "Ocurrió un error al eliminar la oferta." });
            }
        }
    }
}
