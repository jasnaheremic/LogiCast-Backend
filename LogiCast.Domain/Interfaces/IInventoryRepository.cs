using LogiCast.Domain.DTOs;

namespace LogiCast.Domain.Interfaces;

public interface IInventoryRepository
{
    Task<InventoryDto> CreateInventoryAsync(CreateInventoryDto createInventoryDto);
}