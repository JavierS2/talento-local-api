using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OffersController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpPost("Mockup")]
        public async Task<ActionResult<Offer>> CreateMockup()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Offer off = new ()
            {
                Title = "Prácticas Profesionales - Desarrollo de Software",
                SubTitle = "Apoyo en proyectos internos y desarrollo de nuevas funcionalidades",
                Description = "Buscamos estudiantes de Ingeniería de Sistemas o carreras afines para apoyar en el desarrollo, mantenimiento y documentación de aplicaciones internas. El practicante participará en proyectos reales, aplicando buenas prácticas de programación y trabajando en equipo con desarrolladores senior.",
                Modality = "Virtual",
                Salary = 1900000,
                Requeriments = "Conocimientos básicos en programación orientada a objetos, Java o Python, manejo de bases de datos SQL, control de versiones con Git, y habilidades para el trabajo en equipo.",
                Benefits = "Flexibilidad horaria, posibilidad de trabajo remoto, acompañamiento de mentores, y opción de vinculación laboral al finalizar las prácticas.",
                YearsExperience = 2,
                Location = "Santa Marta, Magdalena",
                Journey = "Tiempo completo",
                AvailablePlaces = 3,
                Status = "Abierta",
                ContractType = "Pasantía",
                PaymentType = "Mensual",
                PublicationDate = DateTime.Now,
                ClosingDate = DateTime.Now.AddDays(15),
                CompanyId = 1,
                CategoryId = 1,
            };

            var offer = await _offerService.CreateAsync(off);
            return CreatedAtAction(nameof(GetById), new { id = offer.Id }, offer);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Offer off)
        {
            try
            {
                if (off == null) return NotFound(new { message = "Failed creating the offer (Offer data is null)" });
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var offer = await _offerService.CreateAsync(off);
                return CreatedAtAction(nameof(GetById), new { id = offer.Id }, offer);

            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating offer", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Offer>>> GetAll()
        {
            try
            {
                var offer = await _offerService.GetAllAsync();
                if(offer == null) return NotFound(new { message = "Failed fetching the offers (Offers not found)" });
                return Ok(offer);
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching offers", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Offer>> GetById(int id)
        {
            try
            {
                var result = await _offerService.GetByIdAsync(id);
                if (result == null) return NotFound(new { message = "Failed fetching the offer (Offer not found)" });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching offer", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Offer>> Updated(int id,[FromBody] Offer offer)
        {
            try
            {
                if (offer == null || id < 0) return NotFound(new { message = "Failed to update the offer (Offer not found)" });

                var updated = await _offerService.UpdateAsync(id, offer);
                return updated ? Ok(updated) : NotFound(new { message = "Failed to update the offer " });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error applying offer update", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _offerService.DeleteAsync(id);
                return (!deleted) ? NotFound(new { message = "Failed deleting the offer (Offer not found)" }) : Ok(id);
     
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting offer", error = ex.Message });
            }
        }
    }
}
