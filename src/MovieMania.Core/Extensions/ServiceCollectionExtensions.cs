using MovieMania.Core.Services;
using MovieMania.Core.Services.Interfaces;

namespace MovieMania.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPaginationService, PaginationService>();

        // Service URI
        services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUriService, UriService>(
            context =>
            {
                IHttpContextAccessor accessor = context.GetRequiredService<IHttpContextAccessor>();
                return new UriService(accessor);
            });

        return services;
    }
}
