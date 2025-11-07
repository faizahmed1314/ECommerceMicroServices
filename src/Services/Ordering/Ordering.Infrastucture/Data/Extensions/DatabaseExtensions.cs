using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastucture.Data.Extensions
{
    public static class DatabaseExtensions
    {
        // Extension methods for database operations can be added here in the future
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            // Implementation for database initialization can be added here
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();
            //await Task.CompletedTask;
        }
    }
}
