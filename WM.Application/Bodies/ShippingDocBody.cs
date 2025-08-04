using WM.Domain.Entities;

namespace WM.Application.Bodies;

public class ShippingDocBody
{
    public string Number { get; set; }
    public ClientBody Client { get; set; }
    public DateTime Date { get; set; }
    public State State {  get; set; }
    public ShippingResBody ResBody { get; set; }
}
