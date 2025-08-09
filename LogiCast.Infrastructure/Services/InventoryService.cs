using LogiCast.Domain.DTOs;
using LogiCast.Domain.Enums;
using LogiCast.Domain.Interfaces;
using LogiCast.Infrastructure.Data;
using LogiCast.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogiCast.Infrastructure.Services;

public class InventoryService(
    IInventoryRepository inventoryRepository,
    AppDbContext appDbContext): IInventoryService
{
    public async Task<InventoryDto> CreateInventoryAsync(CreateInventoryDto createInventoryDto)
    {
        try
        {
            var inventory = await inventoryRepository.CreateInventoryAsync(createInventoryDto);
            await appDbContext.SaveChangesAsync();
            return inventory;
        }
        catch
        {
            throw new Exception("Unable to create inventory item");
        }
    }
    
   public async Task<IEnumerable<WarehouseInventoryDto>> GetInventoryByWarehouseIdAsync(Guid warehouseId)
   {
       if (warehouseId == Guid.Empty)
           throw new ArgumentException("Invalid warehouse ID");

       var inventoryItems = await inventoryRepository.GetInventoryWithItemAndCategoryByWarehouseIdAsync(warehouseId);

       var warehouseInventory = inventoryItems.Select(i => new WarehouseInventoryDto
       {
           ItemId = i.Item.Id,
           Barcode = i.Item.Barcode,
           ItemName = i.Item.Name,
           CategoryName = i.Item.Category?.Name ?? "Unknown",
           Quantity = i.Quantity,
           Price = i.Item.Price,
           Status = CalculateStatus(i.Quantity, i.minValue, i.maxValue)
       });

       return warehouseInventory;
   }

   public async Task<IEnumerable<TotalInventoryDto>> GetAllInventoryAsync()
   {
    var allItems = await appDbContext.Item
        .Include(i => i.Category)
        .ToListAsync();

    var inventoryItems = await appDbContext.Inventory
        .Include(i => i.Item)
        .ToListAsync();

    var inventoryGrouped = inventoryItems
        .GroupBy(i => i.Item.Id)
        .ToDictionary(
            g => g.Key,
            g => new
            {
                TotalQuantity = g.Sum(i => i.Quantity),
                TotalPrice = g.Sum(i => i.Quantity * i.Item.Price)
            });
    
    var totalInventory = allItems.Select(item =>
    {
        var hasInventory = inventoryGrouped.TryGetValue(item.Id, out var summary);

        return new TotalInventoryDto
        {
            ItemId = item.Id,
            Barcode = item.Barcode,
            ItemName = item.Name,
            CategoryName = item.Category?.Name ?? "Unknown",
            TotalQuantity = hasInventory ? summary.TotalQuantity : 0,
            TotalPrice = hasInventory ? Math.Round(summary.TotalPrice, 2) : 0.0
        };
    });

    return totalInventory;
   }
   
   public async Task<InventoryDashboardDto> GetInventoryDashboardAsync()
   {
       var inventoryDashboardDto = new InventoryDashboardDto
       {
           TotalInventoryValue = await inventoryRepository.GetTotalInventoryValueAsync(),
           LowStockItemsCount = await inventoryRepository.GetLowStockItemsCountAsync(),
           TotalCategoriesCount = await inventoryRepository.GetTotalCategoriesCountAsync(),
           TotalItemsCount = await inventoryRepository.GetTotalItemsCountAsync()
       };

       return inventoryDashboardDto;
   }

   public async Task<List<CategorySumDto>> GetTopThreeCategoriesAsync()
   {
       var allCategories = await inventoryRepository.GetCategorySumValuesAsync();
       return allCategories.Take(3).ToList();
   }

   public async Task<IEnumerable<LowStockItemDto>> GetLowStockItemsAsync()
   {
       var lowStockItems = await inventoryRepository.GetLowStockItemsAsync();
       return lowStockItems;
   }

   private string CalculateStatus(int quantity, int min, int max)
   {
       if (quantity == 0)
           return ItemStatus.Alert.ToString();
       if (quantity < min)
           return ItemStatus.BelowMin.ToString();
       if (quantity >= min && quantity < max)
           return ItemStatus.Normal.ToString();
       if (quantity >= max)
           return ItemStatus.Max.ToString();

       return ItemStatus.Normal.ToString();
   }
}