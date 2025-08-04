namespace LogiCast.Domain.DTOs;

public class WarehouseInventoryDto
{
    public Guid ItemId { get; set; }
    public string Barcode { get; set; }
    public string ItemName { get; set; }
    public string CategoryName { get; set; }
    public int Quantity { get; set; }
    public string Status { get; set; }
    public double Price { get; set; } 
}
