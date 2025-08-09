namespace LogiCast.Domain.DTOs;

public class LowStockItemDto
{
    public Guid WarehouseId { get; set; }
    public string WarehouseName { get; set; }
    public Guid ItemId { get; set; }
    public string ItemName { get; set; }
    public int CurrentStock { get; set; }
    public int ReorderQuantity { get; set; }
}