using Microsoft.AspNetCore.Mvc;
using TalentoLocal.Models;
using TalentoLocal.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TalentoLocal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferCategoryController(IOfferCategoryService offerCategoryService) : ControllerBase
    {
        private readonly IOfferCategoryService _offerCategoryService = offerCategoryService;


        [HttpPost("Mockup")]
        public async Task<ActionResult<int>> CreateMockup()
        {
            OfferCategory off = new()
            {
                Name = "Desarrollo de Software"
            };

            var offCreate = await _offerCategoryService.CreateAsync(off);
            return CreatedAtAction(nameof(GetById), new {id = offCreate.Id}, offCreate);

        }

        [HttpGet]
        public async Task<ActionResult<List<OfferCategory>>> GetAll()
        {
            try
            {
                var categories = await _offerCategoryService.GetAllAsync();
                if (categories == null)
                {
                    return NotFound(new { message = "Failed feching offer categories (Categories not found)" });
                }
                return Ok(categories);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching offer categories", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfferCategory>> GetById(int id)
        {
            try
            {
                if (id == -1) return NotFound(new {message = "Failed feching offer category (Category ID not found)" });
                var category = await _offerCategoryService.GetByIdAsync(id);
                return Ok(category);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching offer category ID", error = ex.Message });
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<OfferCategory>> Create([FromBody] OfferCategory value)
        {
            try
            {
                if (value == null) return NotFound(new { message = "Failed creating offer category (Category is null)" });

                var newCategory = await _offerCategoryService.CreateAsync(value);
                return CreatedAtAction(nameof(GetById), new { id = newCategory.Id }, newCategory ); ;

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating offer category", error = ex.Message });
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OfferCategory>> Put(int id, [FromBody] OfferCategory value)
        {
            try
            {
                if (value == null && id == -1) NotFound(new { message = "Failed updating offer category (Category is null)" });

                var updateCategory = await _offerCategoryService.UpdateAsync(id, value);

                return updateCategory ? Ok(updateCategory) : NotFound(new { message = "Failed updating offer category (Category is null)"});

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating offer category", error = ex.Message });
            }

            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id == -1 || id < 0) return NotFound(new { message = "Failed deleting offer category (Category is null)" });

                var deleteCategory = await _offerCategoryService.DeleteAsync(id);

                return deleteCategory ? Ok(deleteCategory) : NotFound(new { message = "Failed deleting offer category" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting offer category", error = ex.Message });
            }
            
        }
    }
}
