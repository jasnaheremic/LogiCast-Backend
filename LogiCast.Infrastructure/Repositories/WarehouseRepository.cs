using AutoMapper;
using LogiCast.Domain.DTOs;
using LogiCast.Domain.Interfaces;
using LogiCast.Domain.Models;
using LogiCast.Infrastructure.Data;

namespace LogiCast.Infrastructure.Repositories;

public class WarehouseRepository(
    AppDbContext appDbContext,
    IMapper mapper) : IWarehouseRepository
{
    public async Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto createWarehouseDto)
    {
        var warehouse = mapper.Map<Warehouse>(createWarehouseDto);
        warehouse.Id = Guid.NewGuid();
        await appDbContext.Warehouse.AddAsync(warehouse);
        return mapper.Map<WarehouseDto>(warehouse);
    }
}