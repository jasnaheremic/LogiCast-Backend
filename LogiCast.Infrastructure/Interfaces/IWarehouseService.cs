using LogiCast.Domain.DTOs;

namespace LogiCast.Infrastructure.Interfaces;

public interface IWarehouseService
{
    Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto createWarehouseDto);
}