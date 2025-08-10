using WM.Domain.Entities;

namespace WM.Application.Bodies;

public class ShippingDocBody
{
    public string Number { get; set; } = string.Empty;
    public ClientBody Client { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Status { get; set; } = string.Empty;
    public ShippingResBody ResBody { get; set; } = null!;
}
