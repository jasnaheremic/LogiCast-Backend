using LogiCast.Domain.DTOs;
using LogiCast.Domain.Interfaces;
using LogiCast.Infrastructure.Data;
using LogiCast.Infrastructure.Interfaces;

namespace LogiCast.Infrastructure.Services;

public class CategoryService(
    ICategoryRepository categoryRepository, 
    AppDbContext appDbContext) : ICategoryService
{
    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        try
        {
            var category = await categoryRepository.CreateCategoryAsync(createCategoryDto);
            return category;
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to create category: {ex.Message}", ex);
        }
    }

    public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await categoryRepository.GetAllCategoriesAsync();
        return categories;
    }
}