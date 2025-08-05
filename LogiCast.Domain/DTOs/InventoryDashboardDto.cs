namespace LogiCast.Domain.DTOs;

  public class InventoryDashboardDto
    {
        public double TotalInventoryValue { get; set; }
        public int LowStockItemsCount { get; set; }
        public int TotalCategoriesCount { get; set; }
        public int TotalItemsCount { get; set; }
    }
