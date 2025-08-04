using WM.Domain.Entities;

namespace WM.Application.Bodies;

public class ResourceBody
{
    public string Name { get; set; } = string.Empty;
    public State State { get; set; } 
}
