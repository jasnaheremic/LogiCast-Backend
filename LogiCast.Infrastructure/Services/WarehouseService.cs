using LogiCast.Domain.DTOs;
using LogiCast.Domain.Interfaces;
using LogiCast.Infrastructure.Data;
using LogiCast.Infrastructure.Interfaces;

namespace LogiCast.Infrastructure.Services;

public class WarehouseService(
    IWarehouseRepository warehouseRepository,
    AppDbContext appDbContext) : IWarehouseService
{
    public async Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto createWarehouseDto)
    {
        try
        {
            var warehouse = await warehouseRepository.CreateWarehouseAsync(createWarehouseDto);
            await appDbContext.SaveChangesAsync();
            return warehouse;
        }
        catch
        {
            throw new Exception("Unable to create warehouse");
        }
    }

    public async Task<List<WarehouseDto>> GetAllWarehousesAsync()
    {
        var warehouses = await warehouseRepository.GetAllWarehousesAsync();
        return warehouses;
    }

    public async Task<WarehouseDto?> GetWarehouseByIdAsync(Guid warehouseId)
    {
        var warehouse = await warehouseRepository.GetWarehouseByIdAsync(warehouseId); 
        if (warehouse is null)
        {
            throw new Exception($"Warehouse with ID: {warehouseId} not found");
        }
        return warehouse;
    }

    public async Task<List<WarehouseCapacityDto>> GetTopThreeWarehousesByCapacityAsync()
    {
        var warehouses = await warehouseRepository.GetAllWarehousesWithInventoryAsync();

        var topThree = warehouses
            .Select(w =>
            {
                int usedCapacity = w.InventoryItems.Sum(i => i.Quantity);
                double percentage = w.MaxCapacity == 0 ? 0 : (usedCapacity * 100.0) / w.MaxCapacity;

                return new WarehouseCapacityDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    Location = w.Location,
                    MaxCapacity = w.MaxCapacity,
                    UsedCapacity = usedCapacity,
                    CapacityUsagePercent = Math.Round(percentage, 2)
                };
            })
            .OrderByDescending(w => w.CapacityUsagePercent)
            .Take(3)
            .ToList();

        return topThree;    
    }
}