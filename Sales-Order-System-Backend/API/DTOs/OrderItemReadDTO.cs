namespace Sales_Order_System_Backend.API.DTOs;

public class OrderItemReadDTO
{
    public long OrderItemId { get; set; }
    public long OrderId { get; set; }
    public long ItemId { get; set; }
    public string Description { get; set; }
    public string Note { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TaxRate { get; set; }
    public decimal ExclAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal InclAmount { get; set; }
}