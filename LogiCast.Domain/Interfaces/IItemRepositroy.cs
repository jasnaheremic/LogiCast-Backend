using LogiCast.Domain.DTOs;
using LogiCast.Domain.Models;

namespace LogiCast.Domain.Interfaces;

public interface IItemRepositroy
{
    Task<ItemDto> CreateItemAsync(CreateItemDto createItemDto);
    Task<List<ItemDto>> GetAllItemsAsync();
    Task<Item?> GetItemByIdAsync(Guid itemId);
    Task DeleteItemAsync(Item item);
    Task<ItemDto?> UpdateItemAsync(Guid itemId, CreateItemDto updateItemDto);
}