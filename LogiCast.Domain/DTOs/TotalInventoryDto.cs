namespace LogiCast.Domain.DTOs;

public class TotalInventoryDto
{
    public Guid ItemId { get; set; }
    public string Barcode { get; set; }
    public string ItemName { get; set; }
    public string CategoryName { get; set; }
    public int TotalQuantity { get; set; }
    public double TotalPrice { get; set; } 
}