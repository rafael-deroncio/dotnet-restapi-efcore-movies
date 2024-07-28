using System.Net;
using MovieMania.Domain.Enums;

namespace MovieMania.Core.Exceptions;

public class EntityBadRequestException : BaseException
{
    public EntityBadRequestException(string title, string message) : base(title, message, HttpStatusCode.NotFound)
    {
        Title = title;
        Code = HttpStatusCode.BadRequest;
        Type =  ResponseType.Error;
    }
}