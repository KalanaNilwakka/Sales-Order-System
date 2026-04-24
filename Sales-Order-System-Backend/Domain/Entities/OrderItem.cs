using System.ComponentModel.DataAnnotations;

namespace Sales_Order_System_Backend.Domain.Entities;

public class OrderItem
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int ItemId { get; set; }
    public string Description { get; set; }
    public string Note { get; set; }
    [Required]
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TaxRate { get; set; }
    public decimal ExclAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal InclAmount { get; set; }
    
    public Order Order { get; set; }
    public Item Item { get; set; }
}