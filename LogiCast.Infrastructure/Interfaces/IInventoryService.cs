using LogiCast.Domain.DTOs;

namespace LogiCast.Infrastructure.Interfaces;

public interface IInventoryService
{
    Task<InventoryDto> CreateInventoryAsync(CreateInventoryDto createInventoryDto);
}