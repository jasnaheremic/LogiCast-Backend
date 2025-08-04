namespace LogiCast.Domain.DTOs;

public class CreateInventoryDto
{
    public required Guid WarehouseId { get; set; }
    public required Guid ItemId { get; set; }
    public required int Quantity { get; set; }
    public required int maxValue { get; set; }
    public required int minValue { get; set; }
}