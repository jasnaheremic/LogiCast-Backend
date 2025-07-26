using LogiCast.Domain.DTOs;

namespace LogiCast.Infrastructure.Interfaces;

public interface IItemService
{
    Task<ItemDto> CreateItemAsync(CreateItemDto createItemDto);
    Task<List<ItemDto>> GetAllItemsAsync();
    Task<ItemDto?> GetItemByIdAsync(Guid itemId);
}