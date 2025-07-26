using AutoMapper;
using LogiCast.Domain.DTOs;
using LogiCast.Domain.Models;

namespace LogiCast.Infrastructure.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Warehouse, CreateWarehouseDto>().ReverseMap();
        CreateMap<Warehouse, WarehouseDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Item, CreateItemDto>().ReverseMap();
        CreateMap<Item, ItemDto>().ReverseMap();
        CreateMap<Inventory, CreateInventoryDto>().ReverseMap();
        CreateMap<Inventory, InventoryDto>().ReverseMap();
    }
}