namespace WM.Domain.Entities;

public class AdmissionDocEntity: BaseEntity
{
    public string Number { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public AdmissionResEntity? AdmissionRes { get; set; } 
}
