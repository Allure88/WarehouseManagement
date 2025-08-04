namespace WM.Domain.Entities;

public class ShippingDocEntity: BaseEntity
{
    public string Number { get; set; } = string.Empty;

    public long ClientId {  get; set; }
    public ClientEntity Client { get; set; } = null!;
    public DateTime Date { get; set; }
    public DocumentStatus Status { get; set; }
    public ShippingResEntity ShippingRes { get; set; } = null!;
}
