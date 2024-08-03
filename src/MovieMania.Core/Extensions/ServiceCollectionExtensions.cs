using MovieMania.Core.Configurations.Mapper;
using MovieMania.Core.Configurations.Mapper.Interfaces;
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
        services.AddSingleton<IDepartmentService, DepartmentService>();
        services.AddSingleton<IGenreService, GenreService>();
        services.AddSingleton<IGenderService, GenderService>();
        services.AddSingleton<IKeywordService, KeywordService>();
        services.AddSingleton<IProductionCompanyService, ProductionCompanyService>();
        services.AddSingleton<IPersonService, PersonService>();
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
                provider.GetService<MovieManiaContext>(),
                provider.GetService<IDatabaseMemory>() ?? null
            ));

        services.AddSingleton<IBaseRepository<DepartmentEntity>, BaseRepository<DepartmentEntity>>(
            provider => new BaseRepository<DepartmentEntity>(
                provider.GetService<MovieManiaContext>(),
                provider.GetService<IDatabaseMemory>() ?? null
            ));

        services.AddSingleton<IBaseRepository<GenreEntity>, BaseRepository<GenreEntity>>(
            provider => new BaseRepository<GenreEntity>(
                provider.GetService<MovieManiaContext>(),
                provider.GetService<IDatabaseMemory>() ?? null
            ));

        services.AddSingleton<IBaseRepository<GenderEntity>, BaseRepository<GenderEntity>>(
            provider => new BaseRepository<GenderEntity>(
                provider.GetService<MovieManiaContext>(),
                provider.GetService<IDatabaseMemory>() ?? null
            ));

        services.AddSingleton<IBaseRepository<KeywordEntity>, BaseRepository<KeywordEntity>>(
            provider => new BaseRepository<KeywordEntity>(
                provider.GetService<MovieManiaContext>(),
                provider.GetService<IDatabaseMemory>() ?? null
            ));

        services.AddSingleton<IBaseRepository<ProductionCompanyEntity>, BaseRepository<ProductionCompanyEntity>>(
            provider => new BaseRepository<ProductionCompanyEntity>(
                provider.GetService<MovieManiaContext>(),
                provider.GetService<IDatabaseMemory>() ?? null
            ));

        services.AddSingleton<IBaseRepository<PersonEntity>, BaseRepository<PersonEntity>>(
            provider => new BaseRepository<PersonEntity>(
                provider.GetService<MovieManiaContext>(),
                provider.GetService<IDatabaseMemory>() ?? null
            ));

        return services;
    }

    public static IServiceCollection AddInMemoryDatabase(this IServiceCollection services)
    {
        services.AddSingleton<IDatabaseMemory, DatabaseMemory>();
        return services;
    }

    public static IServiceCollection AddObjectConverter(this IServiceCollection services)
    {
        services.AddSingleton<IObjectConverter, ObjectConverter>();
        return services;
    }
}
