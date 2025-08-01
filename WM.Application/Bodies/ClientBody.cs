using WM.Domain.Entities;

namespace WM.Application.Bodies;

public class ClientBody
{
    public string Name { get; set; }
    public string Adress { get; set; }
    public ShippingDocEntity State { get; set; }
}



