using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Messaging.MessTransit
{
    public static class Extension
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
        {
            // Configure MassTransit with RabbitMQ

            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();

                // Register all consumers from the specified assembly
                if (assembly != null)
                {
                    config.AddConsumers(assembly);
                }

                config.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(configuration["MessageBroker: Host"]!), h =>
                    {
                        // You can configure username and password here if needed
                        h.Username(configuration["MessageBroker:UserName"]);
                        h.Password(configuration["MessageBroker:Password"]);
                    });
                    // Configure endpoints for all registered consumers
                    cfg.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}
