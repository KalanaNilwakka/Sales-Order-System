namespace Sales_Order_System_Backend.API.DTOs;

public class OrderReadDTO
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string InvoiceNo { get; set; }
    public DateTime InvoiceDate { get; set; }
    public string ReferenceNo { get; set; }
    public decimal TotalExcl { get; set; }
    public decimal TotalTax { get; set; }
    public decimal TotalIncl { get; set; }

    public List<OrderItemReadDTO> Items { get; set; }
}