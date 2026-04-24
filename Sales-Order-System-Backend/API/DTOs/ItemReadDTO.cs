namespace Sales_Order_System_Backend.API.DTOs;

public class ItemReadDTO
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}