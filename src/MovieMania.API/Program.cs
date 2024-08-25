using Microsoft.EntityFrameworkCore;
using MovieMania.Core.Extensions;
using MovieMania.API.Extensions;
using MovieMania.Core.Contexts;
using Serilog;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureSerilog();

builder.Services.AddEndpointsApiExplorer()
                .AddLowerCaseRouting()
                .AddCors(builder.Configuration)
                .AddApiVersioning(builder.Configuration)
                .AddSwagger(builder.Configuration)
                .AddSwaggerJwtBearer(builder.Configuration)
                .AddAuthentication(builder.Configuration)
                .AddAuthorization()
                .AddServices()
                .AddRepositories()
                .AddInMemoryDatabase()
                .AddObjectConverter()
                .AddDbContext<MovieManiaContext>(options =>
{
    string connection = builder.Configuration.GetConnectionString("MovieManiaConnection");
    string assembly = Assembly.GetExecutingAssembly().GetName().Name;
    options.UseNpgsql(connection, opts => opts.MigrationsAssembly(assembly));
    options.EnableSensitiveDataLogging(false);
    options.LogTo(_ => { }, LogLevel.None);
}, ServiceLifetime.Singleton, ServiceLifetime.Singleton);

WebApplication app = builder.Build();

app.UseSerilogRequestLogging()
    .UseCors()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseApiVersioning()
    .UseSwagger(builder.Configuration)
    .UseGlobalExceptionHandler()
    .UseHsts()
    .UseHttpsRedirection()
    .LoadDatabaseMomory();

app.MapControllers();
app.Run();