namespace LogiCast.Domain.Models;

public class Warehouse : BaseModel
{
    public required string Name { get; set; }
    public required string Location { get; set; }
    public required int MaxCapacity { get; set; }
    public int? UsedCapacity { get; set; }
    public ICollection<Inventory> InventoryItems { get; set; } = new List<Inventory>();
}