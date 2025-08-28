namespace LogiCast.Domain.DTOs;

public class UpdateWarehouseDto
{ 
    public required string Name { get; set; }
    public required string Location { get; set; }
    public required int MaxCapacity { get; set; }
}