namespace WM.Domain.Models;

public class Client
{
    public string Name { get; set; } = string.Empty;
    public string Adress { get; set; } = string.Empty;
    public State State { get; set; }
    public List<ShippingDoc> ShippingDocuments { get; set; } = [];
}
