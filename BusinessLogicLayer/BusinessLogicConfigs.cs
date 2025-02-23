using BusinessLogicLayer.ServiceContracts;
using BusinessLogicLayer.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer;

public static class BusinessLogicConfigs
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(BusinessLogicConfigs).Assembly);

        services.AddAutoMapper(typeof(BusinessLogicConfigs).Assembly);

        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
