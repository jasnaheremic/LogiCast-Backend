using LogiCast.Domain.DTOs;
using LogiCast.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogiCast.API.Controllers;

[Route("api/item")]
[ApiController]
public class ItemController(IItemService itemService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CreateItemDto>> CreateItem(
        [FromBody] CreateItemDto createItemDto)
    {
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
}