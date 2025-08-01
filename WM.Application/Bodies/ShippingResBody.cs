namespace WM.Application.Bodies;

public class ShippingResBody
{
    public ResourceBody Resource { get; set; }
    public UnitBody UnitOfMeasurement { get; set; }
    public float Quantity { get; set; }
}
