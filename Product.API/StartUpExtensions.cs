using MassTransit;
using Product.API.BL;
using Product.API.BO.Interfaces;
using Product.API.DAL;
using Serilog;
using Shared;

namespace Product.API;

public static class StartUpExtensions
{
    private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    //Register all the services
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.AddServiceDefaults();

        builder.ConfigureLogging(builder.Configuration);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
        });

        // Add services to the container.
        builder.Services.AddBusinessLogic();
        builder.Services.AddDataAccessLayer(builder);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register RabbitMQ
        builder.Services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                var configService = context.GetRequiredService<IConfiguration>();
                var connectionString = configService.GetConnectionString("rabbitmq");
                configurator.Host(connectionString);
                configurator.ConfigureEndpoints(context);
            });
        });
    }

    //Configure the HTTP middleware pipeline
    public static void Configure(WebApplication app)
    {
        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseSerilogRequestLogging();
            //IdentityModelEventSource.ShowPII = true;
        }

        app.UseCors(MyAllowSpecificOrigins);

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        // Create databases if they don't exist
        Task.Run(async () =>
        {
            using var scope = app.Services.CreateScope();
            var dataSeeder = scope.ServiceProvider.GetRequiredService<IAdminRepository>();
            await dataSeeder.CreateDatabase();
        });
    }
}
