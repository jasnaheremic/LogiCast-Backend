namespace LogiCast.Domain.DTOs;

public class CreateWarehouseDto
{
    public string Name { get; set; }
    public string Location { get; set; }
    public int UsedCapacity { get; set; }
    public int MaxCapacity { get; set; }
}