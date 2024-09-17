using Basket.API.BO.Interfaces;
using Basket.API.DAL.Repositories;

namespace Basket.API.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<DBContext>("basketdb");

        services
            .AddScoped<IBasketRepository, BasketRepository>()
            .AddScoped<IAdminRepository, AdminRepository>();

        return services;
    }
}
