using BuildingBlocks.Exceptions.Handler;

namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            // Add API services registrations here

            services.AddCarter();
            services.AddExceptionHandler<CustomExceptionHandler>();
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            // Configure the HTTP request pipeline for API services here
            app.MapCarter();
            app.UseExceptionHandler(options =>
            {
            });
            return app;
        }
    }
}
