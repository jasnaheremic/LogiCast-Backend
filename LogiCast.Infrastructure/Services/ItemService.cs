using LogiCast.Domain.DTOs;
using LogiCast.Domain.Interfaces;
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

    public async Task<ItemDto?> GetItemByIdAsync(Guid itemId)
    {
        var item = await itemRepository.GetItemByIdAsync(itemId); 
        if (item is null)
        {
            throw new Exception($"Item with ID: {itemId} not found");
        }
        return item;
    }
}