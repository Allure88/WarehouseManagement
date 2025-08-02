namespace WM.Domain.Entities;

public class ResourceEntity : BaseEntity
{
    public string Name { get; set; }
    public StateEntity State { get; set; }
}
