using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastucture.Data.Interceptor;


namespace Ordering.Infrastucture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add infrastructure services registrations here

            var connectionString = configuration.GetConnectionString("Database");

            // Add service to the container
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.AddInterceptors(new AuditableEntityInterceptor());
                options.UseSqlServer(connectionString);
            });


            //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            return services;
        }
    }
}
