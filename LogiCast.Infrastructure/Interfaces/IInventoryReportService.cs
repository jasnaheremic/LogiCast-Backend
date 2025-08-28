using LogiCast.Domain.DTOs;

namespace LogiCast.Infrastructure.Interfaces;

public interface IInventoryReportService
{
    byte[] GenerateInventoryReport(InventoryReportDto reportDto);
}