using System.Net;
using System.Text.Json;
using MovieMania.Core.Exceptions;
using MovieMania.Core.Extensions;
using MovieMania.Domain.Enums;
using MovieMania.Domain.Responses;

namespace MovieMania.API.Middlewares;

public class GlobalHandlerExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalHandlerExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BaseException ex)
        {
            ExceptionResponse response = new()
            {
                Title = ex.Title,
                Type = ex.Type.GetDescription(),
                Messages = [ex.Message]
            };

            string json = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(json);
        }
        catch (Exception)
        {
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
