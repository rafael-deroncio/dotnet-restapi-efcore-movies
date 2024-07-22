using MovieMania.Core.Services.Interfaces;

namespace MovieMania.Core.Services;

public class UriService(IHttpContextAccessor accessor) : IUriService
{
    private readonly IHttpContextAccessor _accessor = accessor;
    public Uri GetEndpoint()
    {
        return new UriBuilder(
            scheme: _accessor.HttpContext.Request.Scheme,
            host: _accessor.HttpContext.Request.Host.Host,
            port: _accessor.HttpContext.Request.Host.Port.Value,
            pathValue: _accessor.HttpContext.Request.Path
        ).Uri;
    }
}