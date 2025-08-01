namespace WM.Domain.Entities;

public class AdmissionResEntity: BaseEntity
{
    public ResourceEntity Resource { get; set; }
    public UnitEntity UnitOfMeasurement{ get; set; }
    public float Quantity {  get; set; }
}
