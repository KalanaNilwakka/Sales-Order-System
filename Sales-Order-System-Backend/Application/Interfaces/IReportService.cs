using Sales_Order_System_Backend.Domain.Entities;

namespace Sales_Order_System_Backend.Application.Interfaces;

public interface IReportService
{
    byte[] GenerateOrderInvoice(Order order);
}