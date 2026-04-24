using System.ComponentModel.DataAnnotations;

namespace Sales_Order_System_Backend.Domain.Entities;

public class Item
{
    [Key]
    public long Id { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int Price { get; set; }
    
    public List<OrderItem> OrderItems { get; set; } 
}