using AutoMapper;
using LogiCast.Domain.DTOs;
using LogiCast.Domain.Interfaces;
using LogiCast.Domain.Models;
using LogiCast.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LogiCast.Infrastructure.Repositories;

public class WarehouseRepository(
    AppDbContext appDbContext,
    IMapper mapper) : IWarehouseRepository
{
    public async Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto createWarehouseDto)
    {
        var warehouse = mapper.Map<Warehouse>(createWarehouseDto);
        warehouse.Id = Guid.NewGuid();
        await appDbContext.Warehouse.AddAsync(warehouse);
        return mapper.Map<WarehouseDto>(warehouse);
    }

    public async Task<List<WarehouseDto>> GetAllWarehousesAsync()
    {
        var warehouses = await appDbContext.Warehouse.ToListAsync();
        return mapper.Map<List<WarehouseDto>>(warehouses);
    }
    
    public async Task<Warehouse?> GetWarehouseByIdAsync(Guid warehouseId)
    {
        var warehouse = await appDbContext.Warehouse.FirstOrDefaultAsync(w => w.Id == warehouseId);
        return mapper.Map<Warehouse?>(warehouse);
    }

    public async Task<List<WarehouseDto>> GetAllWarehousesWithInventoryAsync()
    {
        var warehousesWithInventory = await appDbContext.Warehouse
            .Include(w => w.InventoryItems)
            .ToListAsync();
        return mapper.Map<List<WarehouseDto>>(warehousesWithInventory);
    }

    public async Task DeleteWarehouseAsync(Warehouse warehouse)
    {
        var relatedInventories = await appDbContext.Inventory
            .Where(inv => inv.WarehouseId == warehouse.Id)
            .ToListAsync();

        if (relatedInventories.Any())
        {
            appDbContext.Inventory.RemoveRange(relatedInventories);
        }

        appDbContext.Warehouse.Remove(warehouse);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<WarehouseDto?> UpdateWarehouseAsync(Guid warehouseId, UpdateWarehouseDto updateDto)
    {
        var existingWarehouse = await appDbContext.Warehouse.FirstOrDefaultAsync(w => w.Id == warehouseId);

        if (existingWarehouse == null)
            return null;

        existingWarehouse.Name = updateDto.Name;
        existingWarehouse.Location = updateDto.Location;
        existingWarehouse.MaxCapacity = updateDto.MaxCapacity;

        appDbContext.Warehouse.Update(existingWarehouse);
        await appDbContext.SaveChangesAsync();

        return new WarehouseDto
        {
            Id = existingWarehouse.Id,
            Name = existingWarehouse.Name,
            Location = existingWarehouse.Location,
            MaxCapacity = existingWarehouse.MaxCapacity,
            UsedCapacity = existingWarehouse.UsedCapacity,
            InventoryItems = existingWarehouse.InventoryItems.Select(inv => new WarehouseInventoryDto
            {
                ItemId = inv.ItemId,
                Quantity = inv.Quantity,
                // map other fields if needed
            }).ToList()
        };
    }
}