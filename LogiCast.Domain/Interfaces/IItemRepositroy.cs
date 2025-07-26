using LogiCast.Domain.DTOs;

namespace LogiCast.Domain.Interfaces;

public interface IItemRepositroy
{
    Task<ItemDto> CreateItemAsync(CreateItemDto createItemDto);
    Task<List<ItemDto>> GetAllItemsAsync();
    Task<ItemDto?> GetItemByIdAsync(Guid itemId);
}