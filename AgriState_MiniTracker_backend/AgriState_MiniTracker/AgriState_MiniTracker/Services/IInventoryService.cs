using AgriState_MiniTracker.Models;

namespace AgriState_MiniTracker.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryItemDto>> GetInventoryItemsAsync(int? locationId, int? categoryId, string? searchQuery);
        Task<InventoryItemDto?> GetInventoryItemByIdAsync(int id);
        Task<InventoryItemDto> CreateInventoryItemAsync(CreateInventoryItemDto dto);
        Task<bool> DeleteInventoryItemAsync(int id);
        Task<InventoryStatsDto> GetStatsAsync();
        Task<IEnumerable<LocationDto>> GetLocationsAsync();
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync(int? locationId);
    }
}
