using System.ComponentModel.DataAnnotations;

namespace Sales_Order_System_Backend.API.DTOs;

public class OrderItemCreateDTO
{
    public long OrderId { get; set; }
    [Required]
    public long ItemId { get; set; }
    public string Description { get; set; }
    public string Note { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public decimal TaxRate { get; set; }
}