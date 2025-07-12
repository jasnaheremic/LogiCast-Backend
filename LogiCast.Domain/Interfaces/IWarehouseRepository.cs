using LogiCast.Domain.DTOs;

namespace LogiCast.Domain.Interfaces;

public interface IWarehouseRepository
{
    Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto createWarehouseDto);
}