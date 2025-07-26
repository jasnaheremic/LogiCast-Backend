using LogiCast.Domain.DTOs;
using LogiCast.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogiCast.API.Controllers;

[Route("api/category")]
[ApiController]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CreateCategoryDto>> CreateCategory(
        [FromBody] CreateCategoryDto createCategoryDto)
    {
        var categoryDto = await categoryService.CreateCategoryAsync(createCategoryDto);
        return CreatedAtAction(nameof(CreateCategory), new { categoryId = categoryDto.Id }, categoryDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<CategoryDto>>> GetAllCategoriesAsync()
    {
        var categoriesDtos = await categoryService.GetAllCategoriesAsync();
        return Ok(categoriesDtos);
    }
}