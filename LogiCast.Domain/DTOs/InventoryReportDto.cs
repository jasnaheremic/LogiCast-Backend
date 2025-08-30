namespace LogiCast.Domain.DTOs;

public class InventoryReportDto
{
        public string WarehouseName { get; set; } = string.Empty;
        public string WarehouseLocation { get; set; } = string.Empty;
        public DateTime PrintedAt { get; set; } = DateTime.UtcNow;

        public List<InventoryItemReportDto> Items { get; set; } = new();
}