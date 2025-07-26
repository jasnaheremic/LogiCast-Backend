using LogiCast.Domain.DTOs;

namespace LogiCast.Infrastructure.Interfaces;

public interface ICategoryService
{
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    Task<List<CategoryDto>> GetAllCategoriesAsync();
}