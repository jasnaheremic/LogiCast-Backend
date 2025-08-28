using LogiCast.Domain.DTOs;
using LogiCast.Domain.Interfaces;
using LogiCast.Domain.Models;
using LogiCast.Infrastructure.Data;
using LogiCast.Infrastructure.Interfaces;

namespace LogiCast.Infrastructure.Services;

public class ItemService(
    IItemRepositroy itemRepository,
    AppDbContext appDbContext) : IItemService
{
    public async Task<ItemDto> CreateItemAsync(CreateItemDto createItemDto)
    {
        try
        {
            var item = await itemRepository.CreateItemAsync(createItemDto);
            await appDbContext.SaveChangesAsync();
            return item;
        }
        catch
        {
            throw new Exception("Unable to create item");
        }
    }

    public async Task<List<ItemDto>> GetAllItemsAsync()
    {
        var items = await itemRepository.GetAllItemsAsync();
        return items;
    }

    public async Task<Item?> GetItemByIdAsync(Guid itemId)
    {
        var item = await itemRepository.GetItemByIdAsync(itemId); 
        if (item is null)
        {
            throw new Exception($"Item with ID: {itemId} not found");
        }
        return item;
    }

    public async Task<bool> DeleteItemByItemIdAsync(Guid itemId)
    {
        var item = await itemRepository.GetItemByIdAsync(itemId);

        if (item == null)
        {
            return false;
        }

        await itemRepository.DeleteItemAsync(item);
        await appDbContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<ItemDto?> UpdateItemByItemIdAsync(Guid itemId, CreateItemDto updateItemDto)
    {
        return await itemRepository.UpdateItemAsync(itemId, updateItemDto);
    }
}