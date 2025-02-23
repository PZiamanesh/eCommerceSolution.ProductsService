using DataAccessLayer.Repositories;
using DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer;

public static class DataAccessConfigs
{
    public static IServiceCollection AddDataAccessLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySQL(configuration.GetConnectionString("MySqlDb")!);
        });

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
