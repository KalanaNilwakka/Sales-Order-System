using System.ComponentModel.DataAnnotations;

namespace Sales_Order_System_Backend.API.DTOs;

public class OrderCreateDTO
{
    [Required]
    public int ClientId { get; set; }
    [Required]
    public string InvoiceNo { get; set; }
    public DateTime InvoiceDate { get; set; }
    public string ReferenceNo { get; set; }
    
    public List<OrderItemCreateDTO> Items { get; set; }
}