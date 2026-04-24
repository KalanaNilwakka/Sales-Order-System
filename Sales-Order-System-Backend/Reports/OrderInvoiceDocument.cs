using Sales_Order_System_Backend.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Sales_Order_System_Backend.Reports;

public class OrderInvoiceDocument : IDocument
{
    private readonly Order _order;

    public OrderInvoiceDocument(Order order)
    {
        _order = order;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(30);

            // 🔹 HEADER
            page.Header()
                .AlignCenter()
                .Text($"INVOICE - {_order.InvoiceNo}")
                .FontSize(20)
                .Bold();

            // 🔹 CONTENT
            page.Content().Column(col =>
            {
                col.Spacing(10);

                // Order Info
                col.Item().Text($"Client ID: {_order.ClientId}");
                col.Item().Text($"Date: {_order.InvoiceDate:yyyy-MM-dd}");
                col.Item().Text($"Reference: {_order.ReferenceNo}");

                col.Item().PaddingTop(10).Text("Items").Bold();

                // 🔹 TABLE
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                    });

                    // HEADER
                    table.Header(header =>
                    {
                        header.Cell().Text("Code").Bold();
                        header.Cell().Text("Description").Bold();
                        header.Cell().Text("Note").Bold();
                        header.Cell().Text("Qty").Bold();
                        header.Cell().Text("Price").Bold();
                        header.Cell().Text("Tax%").Bold();
                        header.Cell().Text("Excl").Bold();
                        header.Cell().Text("Tax").Bold();
                        header.Cell().Text("Incl").Bold();
                    });

                    // ROWS
                    foreach (var item in _order.Items)
                    {
                        table.Cell().Text(item.ItemCode?? "-");
                        table.Cell().Text(item.Description);
                        table.Cell().Text(item.Note ?? "-");
                        table.Cell().Text(item.Quantity.ToString());
                        table.Cell().Text(item.Price.ToString("0.00"));
                        table.Cell().Text((item.TaxRate * 100).ToString("0") + "%");
                        table.Cell().Text(item.ExclAmount.ToString("0.00"));
                        table.Cell().Text(item.TaxAmount.ToString("0.00"));
                        table.Cell().Text(item.InclAmount.ToString("0.00"));
                    }
                });

                // 🔹 TOTALS
                col.Item().PaddingTop(20).Column(summary =>
                {
                    summary.Item().AlignRight().Text($"Subtotal (Excl): {_order.TotalExcl:0.00}");
                    summary.Item().AlignRight().Text($"Total Tax: {_order.TotalTax:0.00}");
                    summary.Item().AlignRight().Text($"Grand Total (Incl): {_order.TotalIncl:0.00}")
                        .Bold().FontSize(14);
                });

                // 🔹 FOOTER NOTE
                col.Item().PaddingTop(20)
                    .AlignCenter()
                    .Text("Thank you for your business!")
                    .Italic();
            });
        });
    }
}