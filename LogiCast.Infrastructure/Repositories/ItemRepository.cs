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

    public async Task<ItemDto?> GetItemByIdAsync(Guid itemId)
    {
        var item = await appDbContext.Item.FirstOrDefaultAsync(i => i.Id == itemId);
        return mapper.Map<ItemDto?>(item);
    }
}