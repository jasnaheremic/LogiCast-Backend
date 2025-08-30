using FluentValidation;
using LogiCast.Domain.DTOs;
using LogiCast.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogiCast.API.Controllers;

[Route("api/warehouse")]
[ApiController]
public class WarehouseController(
    IWarehouseService warehouseService, 
    IInventoryService inventoryService,
    IInventoryReportService inventoryReportService, 
    IValidator<CreateWarehouseDto> validator) : ControllerBase
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
    
    [HttpGet("{warehouseId:Guid}/pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWarehouseInventoryPdf(Guid warehouseId)
    {
        var inventoryItems = await inventoryService.GetInventoryByWarehouseIdAsync(warehouseId);
        var warehouse = await warehouseService.GetWarehouseByIdAsync(warehouseId);

        if (warehouse == null || inventoryItems == null)
            return NotFound();

        // Map to report DTO
        var reportDto = new InventoryReportDto
        {
            WarehouseName = warehouse.Name,
            WarehouseLocation = warehouse.Location,
            PrintedAt = DateTime.UtcNow,
            Items = inventoryItems.Select(i => new InventoryItemReportDto
            {
                Barcode = i.Barcode,
                ItemName = i.ItemName,
                Unit = i.Unit,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        };

        var pdfBytes = inventoryReportService.GenerateInventoryReport(reportDto);

        return File(pdfBytes, "application/pdf", $"{warehouse.Name}_Inventory_{DateTime.UtcNow:yyyyMMdd}.pdf");
    }
}