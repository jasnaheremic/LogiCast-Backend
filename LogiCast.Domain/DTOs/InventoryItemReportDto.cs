using LogiCast.Domain.Enums;

namespace LogiCast.Domain.DTOs;

public class InventoryItemReportDto
{
    public string Barcode { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public ItemUnit Unit { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double TotalPrice => Quantity * Price;
}