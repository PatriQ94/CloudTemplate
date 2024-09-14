using Serilog;
using Serilog.Events;

namespace Shared;

public static class LoggerConfig
{
    public static void ConfigureLogging(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var conf = new Serilog.LoggerConfiguration().ReadFrom.Configuration(configuration);
        conf.ReadFrom.Services(builder.Services.BuildServiceProvider())
            .WriteTo.Console(
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {LogInfo} {Message:lj}{NewLine}{Exception}",
                restrictedToMinimumLevel: LogEventLevel.Information
            );

        Log.Logger = conf.CreateLogger();
        builder.Host.UseSerilog();
    }
}
