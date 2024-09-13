using Basket.API.BO.Interfaces;
using Basket.API.DAL.Repositories;

namespace Basket.API.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBasketRepository, BasketRepository>();

        return services;
    }
}
