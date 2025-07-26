using LogiCast.Domain.Enums;
using LogiCast.Domain.Models;

namespace LogiCast.Domain.DTOs;

public class CreateItemDto
{
    public required string Name { get; set; }
    public required Guid CategoryId { get; set; }
    public required ItemUnit Unit { get; set; }
    public required string Barcode { get; set; }
    public required Double Price { get; set; }
}