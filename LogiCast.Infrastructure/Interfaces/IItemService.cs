using LogiCast.Domain.DTOs;
using LogiCast.Domain.Models;

namespace LogiCast.Infrastructure.Interfaces;

public interface IItemService
{
    Task<ItemDto> CreateItemAsync(CreateItemDto createItemDto);
    Task<List<ItemDto>> GetAllItemsAsync();
    Task<Item?> GetItemByIdAsync(Guid itemId);
    Task<bool> DeleteItemByItemIdAsync(Guid itemId);
    Task<ItemDto?> UpdateItemByItemIdAsync(Guid itemId, CreateItemDto updateItemDto);
}