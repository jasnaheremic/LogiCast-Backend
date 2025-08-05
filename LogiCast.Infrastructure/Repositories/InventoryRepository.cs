using AutoMapper;
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
}