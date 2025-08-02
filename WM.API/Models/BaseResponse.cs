using System.Net;

namespace WM.API.Models;

public class BaseResponse(object? body)
{
    public string Id { get; set; } = string.Empty;
    public bool Success { get; set; }
    public object? Body { get; set; } = body;
    public List<string> Errors { get; set; } = [];
    public HttpStatusCode Code { get;  set; }
}
