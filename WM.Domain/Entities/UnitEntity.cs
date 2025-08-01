namespace WM.Domain.Entities
{
    public class UnitEntity : BaseEntity
    {
        public string UnitDescription { get; set; } = string.Empty;
        public ShippingDocEntity State { get; set; }
    }
}
