using AutoMapper;
using LogiCast.Domain.DTOs;
using LogiCast.Domain.Interfaces;
using LogiCast.Domain.Models;
using LogiCast.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LogiCast.Infrastructure.Repositories;

public class ItemRepository(
    AppDbContext appDbContext,
    IMapper mapper) : IItemRepositroy
{
    public async Task<ItemDto> CreateItemAsync(CreateItemDto createItemDto)
    {
        var item = mapper.Map<Item>(createItemDto);
        item.Id = Guid.NewGuid();
        await appDbContext.Item.AddAsync(item);
        await appDbContext.SaveChangesAsync();
        return mapper.Map<ItemDto>(item);
    }

    public async Task<List<ItemDto>> GetAllItemsAsync()
    {
        var items = await appDbContext.Item.ToListAsync();
        return mapper.Map<List<ItemDto>>(items);
    }

    public async Task<Item?> GetItemByIdAsync(Guid itemId)
    {
        var item = await appDbContext.Item.FirstOrDefaultAsync(i => i.Id == itemId);
        return mapper.Map<Item?>(item);
    }

    public async Task DeleteItemAsync(Item item)
    {
        if (item.InventoryRecords != null && item.InventoryRecords.Any())
        {
            appDbContext.Inventory.RemoveRange(item.InventoryRecords);
        }

        appDbContext.Item.Remove(item);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<ItemDto?> UpdateItemAsync(Guid itemId, CreateItemDto updateItemDto)
    {
        var existingItem = await appDbContext.Item.FirstOrDefaultAsync(i => i.Id == itemId);

        if (existingItem == null)
            return null;

        existingItem.Name = updateItemDto.Name;
        existingItem.CategoryId = updateItemDto.CategoryId;
        existingItem.Unit = updateItemDto.Unit;
        existingItem.Barcode = updateItemDto.Barcode;
        existingItem.Price = updateItemDto.Price;

        appDbContext.Item.Update(existingItem);
        await appDbContext.SaveChangesAsync();

        return new ItemDto
        {
            Id = existingItem.Id,
            Name = existingItem.Name,
            CategoryId = existingItem.CategoryId,
            Unit = existingItem.Unit,
            Barcode = existingItem.Barcode,
            Price = existingItem.Price
        };
    }
}