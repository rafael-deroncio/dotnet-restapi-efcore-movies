using System.Net;
using MovieMania.Domain.Enums;

namespace MovieMania.Core.Exceptions;

public class EntityNotFoundException : BaseException
{
    public EntityNotFoundException(string title, string[] messages) : base(title, messages, HttpStatusCode.NotFound)
    {
        Title = title;
        Code = HttpStatusCode.NotFound;
        Type =  ResponseType.Error;
    }
}
