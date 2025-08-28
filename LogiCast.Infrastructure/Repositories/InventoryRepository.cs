using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogiCast.Domain.DTOs;
using LogiCast.Domain.Interfaces;
using LogiCast.Domain.Models;
using LogiCast.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LogiCast.Infrastructure.Repositories;

public class InventoryRepository(
    AppDbContext appDbContext, 
    IMapper mapper) : IInventoryRepository
{
    public async Task<InventoryDto> CreateInventoryAsync(CreateInventoryDto createInventoryDto)
    {
        var inventory = mapper.Map<Inventory>(createInventoryDto);
        inventory.Id = Guid.NewGuid();
        await appDbContext.Inventory.AddAsync(inventory);
        await appDbContext.SaveChangesAsync();
        return mapper.Map<InventoryDto>(inventory);
    }

    public async Task<IEnumerable<InventoryDto?>> GetInventoryWithItemAndCategoryByWarehouseIdAsync(Guid warehouseId)
    {
        var inventoryItems = await appDbContext.Inventory
            .Include(i => i.Item)
            .ThenInclude(item => item.Category)
            .Where(i => i.WarehouseId == warehouseId)
            .ToListAsync();

        return mapper.Map<IEnumerable<InventoryDto?>>(inventoryItems);
    }

    public async Task<IEnumerable<Inventory?>> GetAllInventoryAsync()
    {
        return await appDbContext.Inventory
            .Include(i => i.Item)
            .ThenInclude(item => item.Category)
            .Include(i => i.Warehouse) 
            .ToListAsync();
    }
    public async Task<double> GetTotalInventoryValueAsync()
    {
        return await appDbContext.Inventory
            .Include(i => i.Item)
            .SumAsync(i => i.Item.Price * i.Quantity);
    }

    public async Task<int> GetLowStockItemsCountAsync()
    {
        return await appDbContext.Inventory
            .Where(i => i.Quantity == 0 || i.Quantity < i.minValue)
            .CountAsync();
    }

    public async Task<int> GetTotalCategoriesCountAsync()
    {
        return await appDbContext.Category.CountAsync();
    }

    public async Task<int> GetTotalItemsCountAsync()
    {
        return await appDbContext.Inventory.SumAsync(i => i.Quantity);
    }

    public async Task<List<CategorySumDto>> GetCategorySumValuesAsync()
    {
        var categorySumValues = await appDbContext.Inventory
            .Where(i => i.Item.Category != null)
            .GroupBy(i => new
            {
                i.Item.Category!.Id,
                i.Item.Category.Name
            })
            .Select(g => new CategorySumDto
            {
                CategoryId = g.Key.Id,
                CategoryName = g.Key.Name,
                TotalValue = g.Sum(i => i.Item.Price * i.Quantity)
            })
            .OrderByDescending(c => c.TotalValue)
            .ToListAsync();
        
        return categorySumValues;
    }

    public async Task<IEnumerable<LowStockItemDto>> GetLowStockItemsAsync()
    {
        var inventoryItems = await appDbContext.Inventory
            .Include(i => i.Warehouse)
            .Include(i => i.Item)
            .Where(i => i.Quantity < i.minValue)
            .ToListAsync();

        return mapper.Map<IEnumerable<LowStockItemDto>>(inventoryItems);
    }

    public async Task<bool> DeleteInventoryItemFromWarehouseAsync(Guid warehouseId, Guid itemId)
    {
        var inventoryItem = await appDbContext.Inventory
            .FirstOrDefaultAsync(i => i.WarehouseId == warehouseId && i.ItemId == itemId);

        if (inventoryItem == null)
            return false;

        appDbContext.Inventory.Remove(inventoryItem);
        await appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<InventoryDto?> UpdateInventoryAsync(Guid warehouseId, Guid itemId, UpdateInventoryDto updateDto)
    {
        var existingInventory = await appDbContext.Inventory
            .FirstOrDefaultAsync(inv => inv.WarehouseId == warehouseId && inv.ItemId == itemId);

        if (existingInventory == null)
            return null;

        existingInventory.Quantity = updateDto.Quantity;
        existingInventory.maxValue = updateDto.MaxValue;
        existingInventory.minValue = updateDto.MinValue;

        appDbContext.Inventory.Update(existingInventory);
        await appDbContext.SaveChangesAsync();

        return new InventoryDto
        {
            Id = existingInventory.Id,
            WarehouseId = existingInventory.WarehouseId,
            ItemId = existingInventory.ItemId,
            Quantity = existingInventory.Quantity,
            maxValue = existingInventory.maxValue,
            minValue = existingInventory.minValue
        };
    }
}