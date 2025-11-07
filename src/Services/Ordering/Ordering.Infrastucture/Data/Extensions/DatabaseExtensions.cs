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
            await SeedAsync(context);
        }

        private static async Task SeedAsync(ApplicationDbContext context)
        {
            // Implementation for seeding initial data can be added here
            await SeedCustomerAsync(context);
            await SeedProductAsync(context);
            await SeedOrderWithItemAsync(context);

        }



        private static async Task SeedCustomerAsync(ApplicationDbContext context)
        {
            if (!await context.Customers.AnyAsync())
            {
                context.Customers.AddRange(InitialData.Customers);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedProductAsync(ApplicationDbContext context)
        {
            if (!await context.Products.AnyAsync())
            {
                context.Products.AddRange(InitialData.Products);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedOrderWithItemAsync(ApplicationDbContext context)
        {
            if (!await context.Orders.AnyAsync())
            {
                context.Orders.AddRange(InitialData.OrderWithItems);
                await context.SaveChangesAsync();
            }
        }
    }


}