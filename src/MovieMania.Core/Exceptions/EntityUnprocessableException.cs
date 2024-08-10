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
}