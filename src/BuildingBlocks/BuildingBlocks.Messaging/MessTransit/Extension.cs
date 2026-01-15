using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Messaging.MessTransit
{
    public static class Extension
    {
        public static IServiceCollection AddMessTransitMessaging(this IServiceCollection services, Assembly? assembly = null)
        {
            // Configure MassTransit with RabbitMQ

            return services;
        }
    }
}
