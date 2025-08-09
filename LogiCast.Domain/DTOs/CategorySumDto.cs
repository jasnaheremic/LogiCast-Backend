namespace LogiCast.Domain.DTOs;

public class CategorySumDto
{ 
    public Guid CategoryId  { get; set; }
    public string CategoryName { get; set; } 
    public double TotalValue { get; set; }
}