namespace Sales_Order_System_Backend.API.DTOs;

public class ClientReadDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Suburb { get; set; }
    public string State { get; set; }
    public int PostCode { get; set; }
}