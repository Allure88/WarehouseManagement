namespace WM.Domain.Entities
{
    public class UnitEntity : BaseEntity
    {
        public string UnitDescription { get; set; } = string.Empty;
        public StateEntity State { get; set; }
    }
}
