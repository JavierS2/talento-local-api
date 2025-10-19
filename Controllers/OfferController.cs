using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;

namespace TalentoLocal.Controllers
{
    [Route("offerapi/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        String _offerService = "";

        public OfferController()
        {
            _offerService = "offerService";
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Offer offer)
        {
            try
            {
                if (offer == null)
                {
                    return BadRequest("Data isn't null");
                }

                var offerId = await _offerService.addOffer(offer);
                return CreatedAtAction(nameof(GetById), new { id = offerId }, new { id = offerId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var offer = await _offerService.GetAll();
                return Ok(offer);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Offer>> GetById(int id)
        {
            try
            {
                return Ok(await _offerService.GetById(id));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error get postulation:", ex.Message);
                return null;
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Offer>> Updated (int id, Offer offer)
        {
            if (offer == null)
            {
                return BadRequest("Data isn't null");

            }
            try
            {
                bool updated = await _offerService.Updated(id, offer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating: ", ex.Message);
                return null;
            }
        }
    }
}
