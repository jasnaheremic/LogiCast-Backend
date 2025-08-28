using LogiCast.Domain.DTOs;
using LogiCast.Domain.Models;

namespace LogiCast.Infrastructure.Interfaces;

public interface IWarehouseService
{
    Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto createWarehouseDto);
    Task<List<WarehouseDto>> GetAllWarehousesAsync();
    Task<Warehouse?> GetWarehouseByIdAsync(Guid warehouseId);
    Task<List<WarehouseCapacityDto>> GetTopThreeWarehousesByCapacityAsync();
    Task<bool> DeleteWarehouseByIdAsync(Guid warehouseId);
    Task<WarehouseDto?> UpdateWarehouseAsync(Guid warehouseId, UpdateWarehouseDto updateDto); // new

}