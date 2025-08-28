using LogiCast.Domain.DTOs;
using LogiCast.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogiCast.API.Controllers;

[Route("api/warehouse")]
[ApiController]
public class WarehouseController(
    IWarehouseService warehouseService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CreateWarehouseDto>> CreateWarehouse(
        [FromBody] CreateWarehouseDto createWarehouseDto)
    {
        var warehouseDto = await warehouseService.CreateWarehouseAsync(createWarehouseDto);
        return CreatedAtAction(nameof(CreateWarehouse), new { warehouseId = warehouseDto.Id }, warehouseDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<WarehouseDto>>> GetAllWarehousesAsync()
    {
        var warehouseDtos = await warehouseService.GetAllWarehousesAsync();
        return Ok(warehouseDtos);
    }

    [HttpGet("{warehouseId:Guid}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<WarehouseDto?>> GetWarehouseByIdAsync(Guid warehouseId)
    {
        var warehouseDto = await warehouseService.GetWarehouseByIdAsync(warehouseId);
        return Ok(warehouseDto);
    }
    
    [HttpGet("top-capacity")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTopThreeWarehousesByCapacity()
    {
        var topWarehouses = await warehouseService.GetTopThreeWarehousesByCapacityAsync();
        return Ok(topWarehouses);
    }
    
    [HttpDelete("{warehouseId:guid}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteWarehouse(Guid warehouseId)
    {
        var result = await warehouseService.DeleteWarehouseByIdAsync(warehouseId);

        if (!result)
        {
            return NotFound(new { message = $"Warehouse with id {warehouseId} not found." });
        }

        return NoContent();
    }
    
    [HttpPut("{warehouseId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateWarehouse(Guid warehouseId, [FromBody] UpdateWarehouseDto updateDto)
    {
        var result = await warehouseService.UpdateWarehouseAsync(warehouseId, updateDto);

        if (result == null)
        {
            return NotFound(new { message = $"Warehouse with id {warehouseId} not found." });
        }

        return Ok(result);
    }
}