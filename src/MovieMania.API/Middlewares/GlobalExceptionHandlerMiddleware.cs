using System.Net;
using System.Text.Json;
using MovieMania.Core.Exceptions;
using MovieMania.Core.Extensions;
using MovieMania.Domain.Enums;
using MovieMania.Domain.Responses;

namespace MovieMania.API.Middlewares;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BaseException ex)
        {
            string[] errors = ex.Message.Split(", ");

            ExceptionResponse response = new()
            {
                Title = ex.Title,
                Type = ex.Type.GetDescription(),
                Messages = errors.Any() ? errors: [ex.Message]
            };

            string json = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)ex.Code;

            await context.Response.WriteAsync(json);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(message: ex.Message, exception: ex);
            ExceptionResponse response = new()
            {
                Title = "Internal Error",
                Type = ResponseType.Fatal.GetDescription(),
                Messages = ["An error occurred while processing the request."]
            };

            string json = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(json);
        }
    }
}
