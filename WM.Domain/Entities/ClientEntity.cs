namespace WM.Domain.Entities;

public class ClientEntity : BaseEntity
{
    public string Name { get; set; }
    public string Adress { get; set; }
    public StateEntity State { get; set; }
}
