using LogiCast.Domain.DTOs;

namespace LogiCast.Infrastructure.Interfaces;

public interface IWarehouseService
{
    Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto createWarehouseDto);
    Task<List<WarehouseDto>> GetAllWarehousesAsync();
    Task<WarehouseDto?> GetWarehouseByIdAsync(Guid warehouseId);
}