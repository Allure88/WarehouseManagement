namespace WM.Application.Bodies;

public class BalanceBody
{
    public ResourceBody Resource { get; set; } = null!;
    public UnitBody UnitOfMeasurement { get; set; } = null!;
    public float Quantity { get; set; }
}



