using System.ComponentModel.DataAnnotations;

namespace Sales_Order_System_Backend.Domain.Entities;

public class Order
{
    [Key]
    public long OrderId { get; set; }
    [Required]
    public long ClientId { get; set; }
    [Required]
    public string InvoiceNo { get; set; }
    public DateTime InvoiceDate { get; set; }
    public string ReferenceNo { get; set; }
    public decimal TotalExcl { get; set; }
    public decimal TotalTax { get; set; }
    public decimal TotalIncl { get; set; }
    
    public Client Client { get; set; }
    public List<OrderItem> Items { get; set; }
}