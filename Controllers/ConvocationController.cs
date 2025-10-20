using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TalentoLocal.Controllers
{
    [Route("convocationapi/[controller]")]
    [ApiController]
    public class ConvocationController : ControllerBase
    {
        String IConvocationService = "_convocationService";
        String _convocationService = "";
        public ConvocationController()
        {
            _convocationService = "ConvocationService";
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateConvocation([FromBody] Convocation convocation)
        {
            try
            {
                if (convocation == null)
                {
                    return BadRequest("Data isn't null");
                }

                var convocationId = await _convocationService.addConvocation(convocation);
                return CreatedAtAction(nameof(GetById), new { id = convocationId }, new {id = convocationId });
            }catch(Exception ex)
            {

            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var convocation = await _convocationService.GetAll();
                return Ok(convocation);
            }
            catch (Exception er)
            {
                return StatusCode(500, $"Error interno: {er.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Convocation>> GetById(int id)
        {
            try
            {
                return Ok(await _convocationService.getByStatus(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Convocation>> Updated(int id, Convocation convocation)
        {
            try
            {
                var update = await _convocationService.Updated(id, convocation);

                if (update == null)
                    return NotFound($"Convocatoria con id {id} no encontrada.");

                return Ok(update);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error interno: {e.Message}");
            }
        }

        [HttpGet("location")] 
        public async Task<ActionResult<List<Convocation>>> SearchByLocation([FromQuery] Convocation criteria)
        {
            try
            {
                var convocation = await _convocationService.SearchConvocationByLocation(criteria);
                return Ok(convocation);

            }catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("availableplaces")]
        public async Task<ActionResult<List<Convocation>>> SearchByAvailablePlaces([FromQuery] Convocation criteria)
        {
            try
            {
                var convocation = await _convocationService.SearchConvocationByAvailablePlaces(criteria);
                return Ok(convocation);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
