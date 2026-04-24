using System.ComponentModel.DataAnnotations;

namespace Sales_Order_System_Backend.API.DTOs;

public class OrderItemCreateDTO
{
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int ItemId { get; set; }
    public string Description { get; set; }
    public string Note { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public decimal TaxRate { get; set; }
}