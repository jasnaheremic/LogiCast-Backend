using LogiCast.Domain.DTOs;
using LogiCast.Infrastructure.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using Document = QuestPDF.Fluent.Document;

public class InventoryReportService : IInventoryReportService
{
    public byte[] GenerateInventoryReport(InventoryReportDto reportDto)
    {
        var pdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);

                page.Header().Text($"INVENTURNA LISTA - {reportDto.WarehouseName}")
                    .SemiBold().FontSize(20).AlignCenter();

                page.Content().Column(col =>
                {
                    col.Item().Text($"Skladiste: {reportDto.WarehouseName}");
                    col.Item().Text($"Adresa: {reportDto.WarehouseLocation}");
                    col.Item().Text($"Na dan: {reportDto.PrintedAt:dd/MM/yyyy}");

                    col.Item().PaddingBottom(10);
                    col.Item().LineHorizontal(1);
                    col.Item().PaddingBottom(10);

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(100);
                            columns.RelativeColumn(2);
                            columns.ConstantColumn(60);
                            columns.ConstantColumn(60);
                            columns.ConstantColumn(80);
                            columns.ConstantColumn(100);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Barkod").SemiBold();
                            header.Cell().Text("Naziv Artikla").SemiBold();
                            header.Cell().Text("Mjerna jedinica").SemiBold();
                            header.Cell().Text("Kolicina").SemiBold();
                            header.Cell().Text("Cijena").SemiBold();
                            header.Cell().Text("Ukupna vrijednost").SemiBold();
                        });

                        foreach (var item in reportDto.Items)
                        {
                            table.Cell().Text(item.Barcode);
                            table.Cell().Text(item.ItemName);
                            table.Cell().Text(item.Unit);
                            table.Cell().Text(item.Quantity.ToString());
                            table.Cell().Text($"{item.Price:C}");
                            table.Cell().Text($"{item.TotalPrice:C}");
                        }
                    });
                });

                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Generisano pomocu LogiCast Sistema ").Italic();
                        text.Span($"© {DateTime.UtcNow.Year}");
                    });
            });
        });

        return pdf.GeneratePdf();
    }
}
