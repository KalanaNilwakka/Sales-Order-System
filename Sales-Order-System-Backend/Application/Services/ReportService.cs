using QuestPDF.Fluent;
using Sales_Order_System_Backend.Application.Interfaces;
using Sales_Order_System_Backend.Domain.Entities;
using Sales_Order_System_Backend.Reports;

namespace Sales_Order_System_Backend.Application.Services;

public class ReportService : IReportService
{
    public byte[] GenerateOrderInvoice(Order order)
    {
        var document = new OrderInvoiceDocument(order);
        return document.GeneratePdf();
    }
}