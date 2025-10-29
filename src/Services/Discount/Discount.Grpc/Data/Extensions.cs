using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public static class Extensions
    {
        public static async Task UseMigrationAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DiscountContext>();

            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            try
            {
                logger.LogInformation("Applying EF Core migrations...");
                await context.Database.MigrateAsync();
                logger.LogInformation("EF Core migrations applied successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error applying EF Core migrations");
                throw; // fail fast so container restarts rather than running with a bad state
            }
        }

        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                logger.LogInformation("Applying EF Core migrations...");
                context.Database.MigrateAsync(); // synchronous
                logger.LogInformation("EF Core migrations applied successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error applying EF Core migrations");
                throw;
            }

            return app;
        }
    }
}
