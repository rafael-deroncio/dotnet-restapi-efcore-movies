using Microsoft.EntityFrameworkCore;
using MovieMania.Core.Extensions;
using MovieMania.API.Extensions;
using MovieMania.Core.Contexts;
using Serilog;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureSerilog();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddLowerCaseRouting();

builder.Services.AddCors(builder.Configuration);

builder.Services.AddApiVersioning(builder.Configuration);

builder.Services.AddSwagger(builder.Configuration);

builder.Services.AddSwaggerJwtBearer(builder.Configuration);

builder.Services.AddAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddDbContext<MovieManiaContext>(options =>
{
    string connection = builder.Configuration.GetConnectionString("MovieManiaConnection");
    string assembly = Assembly.GetExecutingAssembly().GetName().Name;
    options.UseNpgsql(connection, opts => opts.MigrationsAssembly(assembly));
});

WebApplication app = builder.Build();

app.UseSerilogRequestLogging();

app.UseCors();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseApiVersioning();

app.UseSwagger(builder.Configuration);

app.UseGlobalExceptionHandler();

app.UseHsts();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();