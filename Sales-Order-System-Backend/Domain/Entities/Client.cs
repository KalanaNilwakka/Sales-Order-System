using System.ComponentModel.DataAnnotations;

namespace Sales_Order_System_Backend.Domain.Entities;

public class Client
{
    [Key]
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Suburb { get; set; }
    public string State { get; set; }
    public int PostCode { get; set; }
    
    public List<Order> Orders { get; set; }
}