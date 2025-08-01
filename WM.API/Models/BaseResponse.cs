using System.Net;

namespace WM.API.Models;

public class BaseResponse(object? body)
{
    public string Id { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public object? Body { get; set; } = body;
    public HttpStatusCode Code { get;  set; }
}
