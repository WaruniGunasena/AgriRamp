using AgriState_MiniTracker.Data;
using AgriState_MiniTracker.Models;

namespace AgriState_MiniTracker.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<InventoryItemDto>> GetInventoryItemsAsync(int? locationId, int? categoryId, string? searchQuery)
        {
            return _repository.GetAllAsync(locationId, categoryId, searchQuery);
        }

        public Task<InventoryItemDto?> GetInventoryItemByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<InventoryItemDto> CreateInventoryItemAsync(CreateInventoryItemDto dto)
        {
            return _repository.CreateAsync(dto);
        }

        public Task<bool> DeleteInventoryItemAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public Task<InventoryStatsDto> GetStatsAsync()
        {
            return _repository.GetStatsAsync();
        }

        public Task<IEnumerable<LocationDto>> GetLocationsAsync()
        {
            return _repository.GetLocationsAsync();
        }

        public Task<IEnumerable<CategoryDto>> GetCategoriesAsync(int? locationId)
        {
            return _repository.GetCategoriesAsync(locationId);
        }
    }
}
