namespace MovieMania.API.Middlewares;

public class GlobalHandlerExceptionMiddleware
{
    private readonly ILogger<GlobalHandlerExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public GlobalHandlerExceptionMiddleware(RequestDelegate next, ILogger<GlobalHandlerExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception)
        {

        }
    }
}
