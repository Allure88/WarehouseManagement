namespace WM.Application.Responces;

public class BaseCommandResponse
{
    public int Id { get; set; }
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = [];
}
