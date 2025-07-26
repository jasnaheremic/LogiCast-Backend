using AutoMapper;
using LogiCast.Domain.DTOs;
using LogiCast.Domain.Interfaces;
using LogiCast.Domain.Models;
using LogiCast.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LogiCast.Infrastructure.Repositories;

public class CategoryRepository(
    AppDbContext appDbContext, 
    IMapper mapper) : ICategoryRepository
{
    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        var category = mapper.Map<Category>(createCategoryDto);
        category.Id = Guid.NewGuid();
        await appDbContext.Category.AddAsync(category);
        await appDbContext.SaveChangesAsync();
        return mapper.Map<CategoryDto>(category);
    }

    public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await appDbContext.Category.ToListAsync();
        return mapper.Map<List<CategoryDto>>(categories);
    }
}