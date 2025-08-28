namespace LogiCast.Domain.DTOs;

public class UpdateInventoryDto
{
        public required int Quantity { get; set; }
        public required int MaxValue { get; set; }
        public required int MinValue { get; set; }
}