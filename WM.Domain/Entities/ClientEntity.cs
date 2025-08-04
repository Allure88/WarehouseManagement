namespace WM.Domain.Entities;

public class ClientEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Adress { get; set; } = string.Empty;
    public State State { get; set; }
}
