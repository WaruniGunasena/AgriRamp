using AgriState_MiniTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace AgriState_MiniTracker.Data
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryItemDto>> GetAllAsync(int? locationId, int? categoryId, string? searchQuery)
        {
            var query = _context.InventoryItems
                .Include(i => i.Location)
                .Include(i => i.Category)
                .AsQueryable();

            if (locationId.HasValue && locationId.Value > 0)
            {
                query = query.Where(i => i.LocationId == locationId.Value);
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(i => i.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                var search = searchQuery.Trim().ToLower();
                query = query.Where(i => 
                    i.Name.ToLower().Contains(search) ||
                    (i.Location != null && i.Location.Name.ToLower().Contains(search)) ||
                    (i.Category != null && i.Category.Name.ToLower().Contains(search))
                );
            }

            return await query
                .OrderByDescending(i => i.UpdatedAt)
                .Select(i => new InventoryItemDto
                {
                    Id = i.ItemId,
                    Name = i.Name,
                    LocationId = i.LocationId,
                    LocationName = i.Location != null ? i.Location.Name : "",
                    CategoryId = i.CategoryId,
                    CategoryName = i.Category != null ? i.Category.Name : "",
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                    MinThreshold = i.MinStockLevel,
                    Status = i.Quantity <= i.MinStockLevel ? "Low Stock" : "In Stock"
                })
                .ToListAsync();
        }

        public async Task<InventoryItemDto?> GetByIdAsync(int id)
        {
            return await _context.InventoryItems
                .Include(i => i.Location)
                .Include(i => i.Category)
                .Where(i => i.ItemId == id)
                .Select(i => new InventoryItemDto
                {
                    Id = i.ItemId,
                    Name = i.Name,
                    LocationId = i.LocationId,
                    LocationName = i.Location != null ? i.Location.Name : "",
                    CategoryId = i.CategoryId,
                    CategoryName = i.Category != null ? i.Category.Name : "",
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                    MinThreshold = i.MinStockLevel,
                    Status = i.Quantity <= i.MinStockLevel ? "Low Stock" : "In Stock"
                })
                .FirstOrDefaultAsync();
        }

        public async Task<InventoryItemDto> CreateAsync(CreateInventoryItemDto dto)
        {
            var entity = new InventoryItem
            {
                LocationId = dto.LocationId,
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Quantity = dto.Quantity,
                Unit = dto.Unit,
                MinStockLevel = dto.MinThreshold,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.InventoryItems.Add(entity);
            await _context.SaveChangesAsync();

            var createdDto = await GetByIdAsync(entity.ItemId);
            return createdDto ?? throw new InvalidOperationException("Failed to retrieve created item.");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.InventoryItems.FindAsync(id);
            if (item == null)
            {
                return false;
            }

            _context.InventoryItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<InventoryStatsDto> GetStatsAsync()
        {
            var items = await _context.InventoryItems.ToListAsync();

            var totalItems = items.Count;
            var lowStockCount = items.Count(i => i.Quantity <= i.MinStockLevel);
            var totalQuantity = items.Sum(i => i.Quantity);
            var locationsCount = items.Select(i => i.LocationId).Distinct().Count();

            return new InventoryStatsDto
            {
                TotalItems = totalItems,
                LowStockCount = lowStockCount,
                TotalQuantity = totalQuantity,
                LocationsCount = locationsCount
            };
        }

        public async Task<IEnumerable<LocationDto>> GetLocationsAsync()
        {
            return await _context.Locations
                .OrderBy(l => l.Name)
                .Select(l => new LocationDto
                {
                    Id = l.LocationId,
                    Name = l.Name,
                    Code = l.Code
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(int? locationId)
        {
            var query = _context.Categories.AsQueryable();

            if (locationId.HasValue && locationId.Value > 0)
            {
                query = query.Where(c => c.LocationId == locationId.Value);
            }

            return await query
                .OrderBy(c => c.Name)
                .Select(c => new CategoryDto
                {
                    Id = c.CategoryId,
                    LocationId = c.LocationId,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}