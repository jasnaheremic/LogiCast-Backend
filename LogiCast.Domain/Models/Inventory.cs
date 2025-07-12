namespace LogiCast.Domain.Models;

public class Inventory : BaseModel
{
    public required Guid WarehouseId { get; set; }
    public required Guid InventoryItemId { get; set; }
    public required int Quantity { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    public required Warehouse Warehouse { get; set; }
}