namespace WM.Domain.Entities;

public class ShippingDocEntity: BaseEntity
{
    public string Number { get; set; }
    public ClientEntity Client { get; set; }
    public DateTime Date { get; set; }
    public StateEntity State { get; set; }
}
