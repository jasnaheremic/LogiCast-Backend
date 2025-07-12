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

}