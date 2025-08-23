

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
        List<string> errors = [];
        bool isOk = true;


        if (admissionDoc.AdmissionRes is null)
        {
            errors.Add("Отсутствуют ресурсы для изменения");
        }
        else if (admissionDoc.AdmissionRes.Quantity < 1e-4)
        {
            errors.Add("Некорректное количество ресурса.");
        }

        if(errors.Count == 0)
        {
            Quantity += admissionDoc.AdmissionRes!.Quantity;
        }


        return(isOk, errors);
    }
}