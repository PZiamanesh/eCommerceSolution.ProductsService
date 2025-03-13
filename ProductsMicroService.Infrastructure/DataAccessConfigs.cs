using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsMicroService.Core.RepositoryContracts;
using ProductsMicroService.Infrastructure.Repositories;

namespace ProductsMicroService.Infrastructure;

public static class DataAccessConfigs
{
    public static IServiceCollection AddDataAccessLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string templateConnectionString = configuration.GetConnectionString("MySqlDb")!;
        string connectionString = templateConnectionString
            .Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST"))
            .Replace("$MYSQL_PORT", Environment.GetEnvironmentVariable("MYSQL_PORT"))
            .Replace("$MYSQL_DATABASE", Environment.GetEnvironmentVariable("MYSQL_DATABASE"))
            .Replace("$MYSQL_USER", Environment.GetEnvironmentVariable("MYSQL_USER"))
            .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD"));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySQL(connectionString);
        });

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
