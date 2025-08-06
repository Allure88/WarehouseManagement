using WM.Domain.Entities;

namespace WM.Application.Bodies;

public class ClientBody
{
    public string Name { get; set; } = string.Empty;
    public string Adress { get; set; } = string.Empty;
    public State State { get; set; }
}



