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
    
    public async Task<WarehouseDto?> GetWarehouseByIdAsync(Guid warehouseId)
    {
        var warehouse = await appDbContext.Warehouse.FirstOrDefaultAsync(w => w.Id == warehouseId);
        return mapper.Map<WarehouseDto?>(warehouse);
    }

    public async Task<List<WarehouseDto>> GetAllWarehousesWithInventoryAsync()
    {
        var warehousesWithInventory = await appDbContext.Warehouse
            .Include(w => w.InventoryItems)
            .ToListAsync();
        return mapper.Map<List<WarehouseDto>>(warehousesWithInventory);
    }
}