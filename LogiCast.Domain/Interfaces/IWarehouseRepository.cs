using LogiCast.Domain.DTOs;
using LogiCast.Domain.Models;

namespace LogiCast.Domain.Interfaces;

public interface IWarehouseRepository
{
    Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto createWarehouseDto);
    Task<List<WarehouseDto>> GetAllWarehousesAsync();
    Task<Warehouse?> GetWarehouseByIdAsync(Guid warehouseId);
    Task<List<WarehouseDto>> GetAllWarehousesWithInventoryAsync();
    Task DeleteWarehouseAsync(Warehouse warehouse);
    Task<WarehouseDto?> UpdateWarehouseAsync(Guid warehouseId, UpdateWarehouseDto updateDto);
}