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
        public async Task<ActionResult<int>> CreateMockup()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Offer off = new Offer
            {
                Name = "Prácticas Profesionales - Desarrollo de Software",
                Description = "Buscamos estudiantes de ingeniería de sistemas o carreras afines para apoyar en el desarrollo y mantenimiento de aplicaciones internas.",
                Mode = Models.enums.Mode.OnSite,
                Duration = "6 meses",
                SpecificRequirements = "Conocimientos básicos en C#, ASP.NET Core y bases de datos SQL.",
                MaximumQuota = 3,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(15),
                IdConvocation = 1
            };

            var offerId = await _offerService.AddOfferAsync(off);
            return CreatedAtAction(nameof(GetById), new { id = offerId }, new { id = offerId });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Offer offer)
        {
            if (offer == null) return BadRequest("Body is null");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var offerId = await _offerService.AddOfferAsync(offer);
            return CreatedAtAction(nameof(GetById), new { id = offerId }, new { id = offerId });
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var offer = await _offerService.GetAllAsync();
            return Ok(offer);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Offer>> GetById(int id)
        {
            var result = await _offerService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Offer>> Updated(int id, Offer offer)
        {
            if (offer == null) return BadRequest("Body is null");

            var updated = await _offerService.UpdateAsync(id, offer);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _offerService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
