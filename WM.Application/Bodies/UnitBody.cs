using WM.Domain.Entities;

namespace WM.Application.Bodies;

public class UnitBody
{
    public string UnitDescription { get; set; } = string.Empty;
    public State State { get; set; }
}
