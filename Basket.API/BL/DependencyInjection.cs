using Basket.API.BL.Services;
using Basket.API.BO.Interfaces;

namespace Basket.API.BL;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddScoped<IBasketService, BasketService>();

        return services;
    }
}
