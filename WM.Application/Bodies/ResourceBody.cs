using WM.Domain.Entities;

namespace WM.Application.Bodies;

public class ResourceBody
{
    public string Name { get; set; }
    public ShippingDocEntity State { get; set; }
}
