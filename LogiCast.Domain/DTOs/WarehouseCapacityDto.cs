namespace LogiCast.Domain.DTOs;

public class WarehouseCapacityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public int MaxCapacity { get; set; }
    public int UsedCapacity { get; set; }
    public double CapacityUsagePercent { get; set; }
}
