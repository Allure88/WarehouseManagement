namespace WM.Domain.Entities;

public class ShippingDocEntity: BaseEntity
{
    public string Number { get; set; } = string.Empty;
    public ClientEntity Client { get; set; } = null!;
    public DateTime Date { get; set; }
    public State State { get; set; }
    public ShippingResEntity ShippingRes { get; set; } = null!;
}
