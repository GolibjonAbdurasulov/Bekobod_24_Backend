using System.Net;

namespace API.Common;

public class ResponseModelBase
{
    public object? Data { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public ResponseModelBase() { }

    public ResponseModelBase(object? data, int statusCode = 200, string? message = null)
    {
        Data = data;
        StatusCode = statusCode;
        Message = message;
    }

    public ResponseModelBase(string message, HttpStatusCode statusCode)
    {
        Message = message;
        StatusCode = (int)statusCode;
    }

    public ResponseModelBase(Exception ex)
    {
        Message = ex.Message;
        StatusCode = 500;
    }
}
