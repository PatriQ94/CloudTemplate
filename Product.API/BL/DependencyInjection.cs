using Product.API.BL.Services;
using Product.API.BO.Interfaces;

namespace Product.API.BL;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
