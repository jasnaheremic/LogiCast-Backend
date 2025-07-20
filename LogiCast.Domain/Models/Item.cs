namespace LogiCast.Domain.Models;

public class Item : BaseModel
{
    public required string Name { get; set; }
    public required Guid CategoryId { get; set; }
    public required string Unit { get; set; }
    public required string Barcode { get; set; }
    public Category? Category { get; set; }
    public ICollection<Inventory> InventoryRecords { get; set; } = new List<Inventory>();
}