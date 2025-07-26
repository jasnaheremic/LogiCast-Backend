using LogiCast.Domain.Enums;

namespace LogiCast.Domain.DTOs;

public class ItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public ItemUnit Unit { get; set; }
    public string Barcode { get; set; }
    public Double Price { get; set; }
}