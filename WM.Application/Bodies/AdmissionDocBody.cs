namespace WM.Application.Bodies;

public class AdmissionDocBody
{
    public string Number { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public AdmissionResBody ResBody { get; set; } = null!;
}
