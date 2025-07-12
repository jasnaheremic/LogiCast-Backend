namespace LogiCast.Domain.Models;

public class Category : BaseModel
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}