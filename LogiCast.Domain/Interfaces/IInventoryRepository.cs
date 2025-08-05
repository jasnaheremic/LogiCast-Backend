using LogiCast.Domain.DTOs;
using LogiCast.Domain.Models;

namespace LogiCast.Domain.Interfaces;

public interface IInventoryRepository
{
    Task<InventoryDto> CreateInventoryAsync(CreateInventoryDto createInventoryDto);
    Task<IEnumerable<InventoryDto?>> GetInventoryWithItemAndCategoryByWarehouseIdAsync(Guid warehouseId); 
    Task<IEnumerable<Inventory?>> GetAllInventoryAsync();
    Task<double> GetTotalInventoryValueAsync();
    Task<int> GetLowStockItemsCountAsync();
    Task<int> GetTotalCategoriesCountAsync();
    Task<int> GetTotalItemsCountAsync();
}