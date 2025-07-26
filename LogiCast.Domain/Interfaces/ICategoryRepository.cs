using LogiCast.Domain.DTOs;

namespace LogiCast.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    Task<List<CategoryDto>> GetAllCategoriesAsync();
}