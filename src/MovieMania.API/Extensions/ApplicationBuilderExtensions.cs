using MovieMania.Core.Configurations.Settings;
using MovieMania.API.Middlewares;
using MovieMania.Core.Contexts;
using MovieMania.Core.Repositories.Interfaces;
namespace MovieMania.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwagger(this IApplicationBuilder builder, IConfiguration configuration)
    {
        // Load Swagger settings from appsettings.json
        SwaggerSettings swaggerSettings = configuration.GetSection("SwaggerSettings").Get<SwaggerSettings>()
            ?? throw new NullReferenceException("No settings for swagger documentation were found.");

        // Configure Swagger middleware
        builder.UseSwagger(options =>
        {
            options.PreSerializeFilters.Add((document, httpRequest) =>
            {
                var serverUrl = $"{httpRequest.Scheme}://{httpRequest.Host.Value}";
                document.Servers =
                [
                    new() { Url = serverUrl },
                    new() { Url = "https://" + httpRequest.Host.Value }
                ];
            });
        });

        // Configure SwaggerUI
        builder.UseSwaggerUI(options =>
        {
            // Set the default models expand depth
            options.DefaultModelsExpandDepth(-1);

            // Configure the Swagger endpoint using the title and name from settings
            options.SwaggerEndpoint($"/swagger/{swaggerSettings.Name}/swagger.json", swaggerSettings.Title);
        });

        return builder;
    }

    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        return builder;
    }

    public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder builder)
    {
        DatabaseInitializer.Initialize(
            builder.ApplicationServices.GetRequiredService<MovieManiaContext>()
            ).GetAwaiter().GetResult();
        return builder;
    }

    public static IApplicationBuilder LoadDatabaseMomory(this IApplicationBuilder builder)
    {
        builder.ApplicationServices.GetRequiredService<IDatabaseMemory>();
        return builder;
    }
}
