

namespace WM.Domain.Models;

public class Balance
{
    public long ResourceId { get; set; }
    public Resource Resource { get; set; } = null!;
    public long UnitOfMeasurementId { get; set; }
    public Unit UnitOfMeasurement { get; set; } = null!;
    public float Quantity { get; set; }

    public (bool allpied, List<string> errors) Apply(AdmissionDoc admissionDoc)
    {
        throw new NotImplementedException();
    }
}