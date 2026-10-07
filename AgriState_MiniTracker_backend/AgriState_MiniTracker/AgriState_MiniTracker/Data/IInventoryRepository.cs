using AgriState_MiniTracker.Models;

namespace AgriState_MiniTracker.Data
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<InventoryItemDto>> GetAllAsync(int? locationId, int? categoryId, string? searchQuery);
        Task<InventoryItemDto?> GetByIdAsync(int id);
        Task<InventoryItemDto> CreateAsync(CreateInventoryItemDto dto);
        Task<bool> DeleteAsync(int id);
        Task<InventoryStatsDto> GetStatsAsync();
        Task<IEnumerable<LocationDto>> GetLocationsAsync();
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync(int? locationId);
    }
}
