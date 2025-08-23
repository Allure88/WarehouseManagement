namespace WM.Domain.Models;

public class ShippingDoc
{
    public string Number { get; set; } = string.Empty;

    public long ClientId {  get; set; }
    public Client Client { get; set; } = null!;
    public DateTime Date { get; set; }
    public DocumentStatus Status { get; set; }
    public ShippingRes ShippingRes { get; set; } = null!;
}
