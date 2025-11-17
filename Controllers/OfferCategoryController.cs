using Microsoft.AspNetCore.Mvc;
using TalentoLocal.DTOs;
using TalentoLocal.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TalentoLocal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferCategoryController : ControllerBase
    {
        private readonly IOfferCategoryService _service;

        public OfferCategoryController(IOfferCategoryService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllAsync();
            return Ok(categories);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "El ID debe ser un número positivo." });

            try
            {
                var category = await _service.GetByIdAsync(id);
                return Ok(category);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al obtener la categoría de oferta." });
            }
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OfferCategoryDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("El cuerpo de la solicitud no puede estar vacío.");

                if (string.IsNullOrWhiteSpace(dto.Name))
                    return BadRequest("El nombre de la categoría es obligatorio.");

                var created = await _service.CreateAsync(dto);

                return Created("api/OfferCategory", created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] OfferCategoryDTO dto)
        {
            if (id <= 0)
                return BadRequest(new { message = "El ID debe ser un número positivo." });

            if (dto == null)
                return BadRequest(new { message = "El cuerpo de la solicitud no puede estar vacío." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.UpdateAsync(id, dto);
                return result
                    ? Ok(new { message = "Categoría de oferta actualizada correctamente." })
                    : NotFound(new { message = $"No se encontró la categoría con ID {id}." });
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
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la categoría de oferta." });
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "El ID debe ser un número positivo." });

            try
            {
                var result = await _service.DeleteAsync(id);
                return result
                    ? Ok(new { message = "Categoría de oferta eliminada correctamente." })
                    : NotFound(new { message = $"No se encontró la categoría con ID {id}." });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al eliminar la categoría de oferta." });
            }
        }
    }
}
