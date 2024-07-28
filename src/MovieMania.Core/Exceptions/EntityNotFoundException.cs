using System.Net;
using MovieMania.Domain.Enums;

namespace MovieMania.Core.Exceptions;

public class EntityNotFoundException : BaseException
{
    public EntityNotFoundException(string title, string message) : base(title, message, HttpStatusCode.NotFound)
    {
        Title = title;
        Code = HttpStatusCode.NotFound;
        Type =  ResponseType.Error;
    }
}
