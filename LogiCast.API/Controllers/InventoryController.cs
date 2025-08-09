using LogiCast.Domain.DTOs;
using LogiCast.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogiCast.API.Controllers;

[Route("api/inventory")]
[ApiController]
public class InventoryController(IInventoryService inventoryService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CreateInventoryDto>> CreateInventory(
        [FromBody] CreateInventoryDto createInventoryDto)
    {
        var inventoryDto = await inventoryService.CreateInventoryAsync(createInventoryDto);
        return CreatedAtAction(nameof(CreateInventory), new { inventoryId = inventoryDto.Id }, inventoryDto);
    }
    
    [HttpGet("{warehouseId:Guid}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<InventoryDto?>> GetInventoryByWarehouseIdAsync(Guid warehouseId)
    {
        var inventoryItemsDto = await inventoryService.GetInventoryByWarehouseIdAsync(warehouseId);
        return Ok(inventoryItemsDto);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<InventoryDto?>> GetAllInventoryAsync()
    {
        var inventoryItemsDto = await inventoryService.GetAllInventoryAsync();
        return Ok(inventoryItemsDto);
    }
    
    [HttpGet("overview")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<InventoryDashboardDto>> GetDashboard()
    {
        var inventoryDashboardDto = await inventoryService.GetInventoryDashboardAsync();
        return Ok(inventoryDashboardDto);
    }
    
    [HttpGet("top-three-categories")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CategorySumDto>> GetTopThreeCategories()
    {
        var topThreeCategories = await inventoryService.GetTopThreeCategoriesAsync();
        return Ok(topThreeCategories);
    }
    
    [HttpGet("low-stock")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<LowStockItemDto>>> GetLowStockItems()
    {
        var result = await inventoryService.GetLowStockItemsAsync();
        return Ok(result);
    }
}