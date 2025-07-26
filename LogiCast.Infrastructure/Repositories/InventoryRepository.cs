using AutoMapper;
using LogiCast.Domain.DTOs;
using LogiCast.Domain.Interfaces;
using LogiCast.Domain.Models;
using LogiCast.Infrastructure.Data;

namespace LogiCast.Infrastructure.Repositories;

public class InventoryRepository(
    AppDbContext appDbContext, 
    IMapper mapper) : IInventoryRepository
{
    public async Task<InventoryDto> CreateInventoryAsync(CreateInventoryDto createInventoryDto)
    {
        var inventory = mapper.Map<Inventory>(createInventoryDto);
        inventory.Id = Guid.NewGuid();
        await appDbContext.Inventory.AddAsync(inventory);
        await appDbContext.SaveChangesAsync();
        return mapper.Map<InventoryDto>(inventory);
    }
}