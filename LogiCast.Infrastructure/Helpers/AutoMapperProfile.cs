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
    }
}