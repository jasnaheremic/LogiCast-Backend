using FluentValidation;
using LogiCast.Domain.DTOs;
using LogiCast.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogiCast.API.Controllers;

[Route("api/item")]
[ApiController]
public class ItemController(IItemService itemService, IValidator<CreateItemDto> validator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CreateItemDto>> CreateItem(
        [FromBody] CreateItemDto createItemDto)
    {
        var validationResult = await validator.ValidateAsync(createItemDto);
        if (validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var itemDto = await itemService.CreateItemAsync(createItemDto);
        return CreatedAtAction(nameof(CreateItem), new { itemId = itemDto.Id }, itemDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<ItemDto>>> GetAllItemsAsync()
    {
        var itemDtos = await itemService.GetAllItemsAsync();
        return Ok(itemDtos);
    }

    [HttpGet("{itemId:Guid}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ItemDto?>> GetItemByIdAsync(Guid itemId)
    {
        var itemDto = await itemService.GetItemByIdAsync(itemId);
        return Ok(itemDto);
    }
    
    [HttpDelete("{itemId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteWarehouse(Guid itemId)
    {
        var result = await itemService.DeleteItemByItemIdAsync(itemId);

        if (!result)
        {
            return NotFound(new { message = $"Item with id {itemId} not found." });
        }

        return NoContent();
    }
    
    [HttpPut("{itemId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateItem(Guid itemId, [FromBody] CreateItemDto updateItemDto)
    {
        var result = await itemService.UpdateItemByItemIdAsync(itemId, updateItemDto);

        if (result == null)
        {
            return NotFound(new { message = $"Item with id {itemId} not found." });
        }

        return Ok(result);
    }
}