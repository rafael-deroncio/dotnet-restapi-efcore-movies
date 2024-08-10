using System.Net;
using MovieMania.Domain.Enums;

namespace MovieMania.Core.Exceptions;

public class EntityUnprocessableException : BaseException
{
    public EntityUnprocessableException(string title, string[] messages) : base(title, messages, HttpStatusCode.UnprocessableEntity)
    {
        Title = title;
        Code = HttpStatusCode.UnprocessableEntity;
        Type =  ResponseType.Error;
    }

    public EntityUnprocessableException(string title, string message) : base(title, message, HttpStatusCode.UnprocessableEntity)
    {
        Title = title;
        Code = HttpStatusCode.UnprocessableEntity;
        Type =  ResponseType.Error;
    }
}