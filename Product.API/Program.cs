using Product.API;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    //Here we register all the services
    StartUpExtensions.ConfigureServices(builder);

    var app = builder.Build();

    //Here we configure the HTTP middleware pipeline
    StartUpExtensions.Configure(app);

    Log.Information("Product API starting up");
    app.Run();
}
catch (Exception ex)
{
    if (ex is not HostAbortedException)
    {
        Log.Fatal(ex, "Product API failed to start correctly");
    }
}
finally
{
    Log.CloseAndFlush();
}

