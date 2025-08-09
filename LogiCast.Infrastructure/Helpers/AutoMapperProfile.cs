using AutoMapper;
using LogiCast.Domain.DTOs;
using LogiCast.Domain.Enums;
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
        
        CreateMap<Inventory, WarehouseInventoryDto>()
            .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.Item.Id))
            .ForMember(dest => dest.Barcode, opt => opt.MapFrom(src => src.Item.Barcode))
            .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item.Name))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Item.Category != null ? src.Item.Category.Name : "Unknown"))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Item.Price))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                src.Quantity == 0
                    ? ItemStatus.Alert.ToString()
                    : src.Quantity < src.minValue
                        ? ItemStatus.BelowMin.ToString()
                        : src.Quantity >= src.maxValue
                            ? ItemStatus.Max.ToString()
                            : ItemStatus.Normal.ToString()
            ));
        CreateMap<Inventory, LowStockItemDto>()
            .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.WarehouseId))
            .ForMember(dest => dest.WarehouseName, opt => opt.MapFrom(src => src.Warehouse.Name))
            .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId))
            .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item.Name))
            .ForMember(dest => dest.CurrentStock, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.ReorderQuantity, opt => opt.MapFrom(src => src.maxValue - src.Quantity));
    }
}