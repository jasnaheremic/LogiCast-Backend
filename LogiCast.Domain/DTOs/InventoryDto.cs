using LogiCast.Domain.Enums;
using LogiCast.Domain.Models;

namespace LogiCast.Domain.DTOs;

public class InventoryDto
{
    public Guid Id { get; set; }
    public required Guid WarehouseId { get; set; }
    public required Guid ItemId { get; set; }
    public ItemStatus Status { get; set; }
    public required int Quantity { get; set; }
    public required int maxValue { get; set; }
    public required int minValue { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    public required Item Item { get; set; } 
}