namespace WM.Domain.Entities;

public class BalanceEntity : BaseEntity
{
    public long ResourceId { get; set; }
    public ResourceEntity Resource { get; set; } = null!;
    public long UnitOfMeasurementId { get; set; }
    public UnitEntity UnitOfMeasurement { get; set; } = null!;
    public float Quantity { get; set; }
}