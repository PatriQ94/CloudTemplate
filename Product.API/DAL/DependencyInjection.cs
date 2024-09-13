﻿using Product.API.BO.Interfaces;
using Product.API.DAL.Repositories;

namespace Product.API.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
