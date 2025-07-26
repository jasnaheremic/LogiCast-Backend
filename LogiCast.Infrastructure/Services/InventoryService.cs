using LogiCast.Domain.DTOs;
using LogiCast.Domain.Interfaces;
using LogiCast.Infrastructure.Data;
using LogiCast.Infrastructure.Interfaces;

namespace LogiCast.Infrastructure.Services;

public class InventoryService(
    IInventoryRepository inventoryRepository,
    AppDbContext appDbContext): IInventoryService
{
    public async Task<InventoryDto> CreateInventoryAsync(CreateInventoryDto createInventoryDto)
    {
        try
        {
            var inventory = await inventoryRepository.CreateInventoryAsync(createInventoryDto);
            await appDbContext.SaveChangesAsync();
            return inventory;
        }
        catch
        {
            throw new Exception("Unable to create inventory item");
        }
    }
}