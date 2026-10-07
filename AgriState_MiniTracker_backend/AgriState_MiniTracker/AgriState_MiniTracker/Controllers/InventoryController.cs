using AgriState_MiniTracker.Models;
using AgriState_MiniTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgriState_MiniTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetItems(
            [FromQuery] int? locationId,
            [FromQuery] int? categoryId,
            [FromQuery] string? searchQuery)
        {
            var items = await _service.GetInventoryItemsAsync(locationId, categoryId, searchQuery);
            return Ok(items);
        }

        [HttpGet("stats")]
        public async Task<ActionResult<InventoryStatsDto>> GetStats()
        {
            var stats = await _service.GetStatsAsync();
            return Ok(stats);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<InventoryItemDto>> GetItem(int id)
        {
            var item = await _service.GetInventoryItemByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryItemDto>> CreateItem([FromBody] CreateInventoryItemDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _service.CreateInventoryItemAsync(dto);
            return CreatedAtAction(nameof(GetItem), new { id = created.Id }, created);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var success = await _service.DeleteInventoryItemAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("locations")]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetLocations()
        {
            var locations = await _service.GetLocationsAsync();
            return Ok(locations);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories([FromQuery] int? locationId)
        {
            var categories = await _service.GetCategoriesAsync(locationId);
            return Ok(categories);
        }
    }
}
