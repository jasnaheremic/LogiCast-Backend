using LogiCast.Domain.DTOs;

namespace LogiCast.Infrastructure.Interfaces;

public interface IInventoryService
{
    Task<InventoryDto> CreateInventoryAsync(CreateInventoryDto createInventoryDto);
    Task<IEnumerable<WarehouseInventoryDto>> GetInventoryByWarehouseIdAsync(Guid warehouseId);
    Task<IEnumerable<TotalInventoryDto>> GetAllInventoryAsync();
    Task<InventoryDashboardDto> GetInventoryDashboardAsync();
    Task<List<CategorySumDto>> GetTopThreeCategoriesAsync();
    Task<IEnumerable<LowStockItemDto>> GetLowStockItemsAsync();
    Task<bool> DeleteInventoryItemFromWarehouseAsync(Guid warehouseId, Guid itemId);
    Task<InventoryDto?> UpdateInventoryAsync(Guid warehouseId, Guid itemId, UpdateInventoryDto updateDto); // new

}