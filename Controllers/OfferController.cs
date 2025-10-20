using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Controllers
{
    [Route("offerapi/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
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
