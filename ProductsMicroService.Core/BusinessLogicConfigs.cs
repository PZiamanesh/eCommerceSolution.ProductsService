using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductsMicroService.Core.RabbitMQ;
using ProductsMicroService.Core.ServiceContracts;
using ProductsMicroService.Core.Services;

namespace ProductsMicroService.Core;

public static class BusinessLogicConfigs
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(BusinessLogicConfigs).Assembly);

        services.AddAutoMapper(typeof(BusinessLogicConfigs).Assembly);

        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();

        return services;
    }
}
