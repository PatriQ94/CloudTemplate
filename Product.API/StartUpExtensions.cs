using Product.API.BL;
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

        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddBusinessLogic(builder.Configuration);
        builder.Services.AddDataAccessLayer(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
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
    }
}
