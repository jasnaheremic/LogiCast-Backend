using LogiCast.Domain.Enums;

namespace LogiCast.Domain.Models;

public class Inventory : BaseModel
{
    public required Guid WarehouseId { get; set; }
    public required Guid ItemId { get; set; }
    public ItemStatus Status { get; set; }
    public required int Quantity { get; set; }
    public required int maxValue { get; set; }
    public required int minValue { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    public required Warehouse Warehouse { get; set; }
}