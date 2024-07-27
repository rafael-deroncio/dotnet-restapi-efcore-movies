using MovieMania.Core.Contexts;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Repositories;
using MovieMania.Core.Repositories.Interfaces;
using MovieMania.Core.Services;
using MovieMania.Core.Services.Interfaces;

namespace MovieMania.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<ICountryService, CountryService>();
        services.AddSingleton<IPaginationService, PaginationService>();

        // Service URI
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IUriService, UriService>(
            context =>
            {
                IHttpContextAccessor accessor = context.GetRequiredService<IHttpContextAccessor>();
                return new UriService(accessor);
            });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IBaseRepository<CountryEntity>, BaseRepository<CountryEntity>>(
            provider => new BaseRepository<CountryEntity>(
                provider.GetService<MovieManiaContext>()
            ));
            
        return services;
    }

    public static IServiceCollection AddInMemoryDatabase(this IServiceCollection services)
    {
        services.AddSingleton<IDatabaseMemory, DatabaseMemory>();
        return services;
    }
}
