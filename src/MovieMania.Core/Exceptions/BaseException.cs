using System.Net;
using MovieMania.Domain.Enums;

namespace MovieMania.Core.Exceptions;

public class BaseException : Exception
{
    public string Title { get; set; }
    public ResponseType Type { get; set; }
    public HttpStatusCode Code { get; set; }

    public BaseException(string title, string[] messages, HttpStatusCode code = HttpStatusCode.Continue)
        : base(string.Join(", ", messages.Select(msg => $"Error: {msg}")))
    {
        Title = title;
        Code = code;
        Type =  (int)code >= 500 ? ResponseType.Fatal :
                (int)code >= 400 ? ResponseType.Warning : ResponseType.Error;
    }

    public BaseException(string title, string[] messages, Exception inner, HttpStatusCode code = HttpStatusCode.Continue)
        : base(string.Join(", ", messages.Select(msg => $"Error: {msg}")), inner)
    {
        Title = title;
        Code = code;
        Type =  (int)code >= 500 ? ResponseType.Fatal :
                (int)code >= 400 ? ResponseType.Warning : ResponseType.Error;
    }

    public BaseException(string[] messages, Exception inner)
        : base(string.Join(", ", messages.Select(msg => $"Error: {msg}")), inner)
    {
        Title = "Error";
        Type = ResponseType.Fatal;
        Code = HttpStatusCode.InternalServerError;
    }
}