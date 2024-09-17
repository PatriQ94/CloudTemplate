using Product.API.BO.Interfaces;
using Product.API.DAL.Repositories;

namespace Product.API.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<DBContext>("productdb");

        services
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IAdminRepository, AdminRepository>();

        return services;
    }
}
