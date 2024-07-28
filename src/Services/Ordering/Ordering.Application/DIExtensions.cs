using Common.Behaviours;
using Common.Exceptions.Handlers;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ordering.Application;

public static class DIExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(assembly);


        return services;
    }
}
