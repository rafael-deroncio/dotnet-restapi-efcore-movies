using Serilog;
using Serilog.Core;

namespace MovieMania.Core.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureSerilog(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, config) =>
            config.MinimumLevel.Information()
                  .MinimumLevel.ControlledBy(new LoggingLevelSwitch())
                  .Enrich.WithProperty("Application", context.Configuration["ApplicationName"])
                  .Enrich.WithProperty("Envioroment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                  .Enrich.FromLogContext()
                  .WriteTo.Console()
                  );
        return hostBuilder;
    }
}
