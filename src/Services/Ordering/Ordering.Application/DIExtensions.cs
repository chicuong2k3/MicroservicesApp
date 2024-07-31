using Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Common.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace Ordering.Application;

public static class DIExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        // Feature Management
        services.AddFeatureManagement();

        // Message Broker
        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

        return services;
    }
}
