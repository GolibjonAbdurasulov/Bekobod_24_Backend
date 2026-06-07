using System.Diagnostics;

namespace WebAPI.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        var method = context.Request.Method;
        var path = context.Request.Path;
        var query = context.Request.QueryString;

        _logger.LogInformation("→ {Method} {Path}{Query}", method, path, query);

        try
        {
            await _next(context);
            sw.Stop();

            var statusCode = context.Response.StatusCode;
            if (statusCode >= 400)
                _logger.LogWarning("← {Method} {Path} → {StatusCode} ({Elapsed}ms)", method, path, statusCode, sw.ElapsedMilliseconds);
            else
                _logger.LogInformation("← {Method} {Path} → {StatusCode} ({Elapsed}ms)", method, path, statusCode, sw.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            sw.Stop();
            _logger.LogError(ex, "✖ {Method} {Path} → Exception ({Elapsed}ms): {Message}", method, path, sw.ElapsedMilliseconds, ex.Message);
            throw;
        }
    }
}
