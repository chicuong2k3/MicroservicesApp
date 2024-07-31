

using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.Messaging.MassTransit
{
    public static class Extensions
    {
        public static IServiceCollection AddMessageBroker(
            this IServiceCollection services,
            IConfiguration configuration,
            Assembly? assembly = null)
        {
            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();

                if (assembly != null)
                {
                    config.AddConsumers(assembly);
                }

                config.UsingRabbitMq((context, rConfig) =>
                {
                    rConfig.Host(new Uri(configuration["RabbitMQ:Host"]!), host =>
                    {
                        host.Username(configuration["RabbitMQ:UserName"]!);
                        host.Password(configuration["RabbitMQ:Password"]!);
                    });

                    rConfig.ConfigureEndpoints(context);
                });

            });

            return services;
        }
    }
}
