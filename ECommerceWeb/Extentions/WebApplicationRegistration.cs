using ECommerce.Domain.Contracts;
using ECommerce.Presistance.Data.DBContexts;
using ECommerce.Presistance.IdentiyData.DbContext;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Extentions
{
    public static class WebApplicationRegistration
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
            await using var scope =  app.Services.CreateAsyncScope();
            var dbcontextService = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var PendingmMigration = await dbcontextService.Database.GetPendingMigrationsAsync();
            if (PendingmMigration.Any())
            {
                await dbcontextService.Database.MigrateAsync();
            }
           return app;
        }

        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
            await using var scope =  app.Services.CreateAsyncScope();
            var dataInitializer = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
            await dataInitializer.InitializeAsync();
            return app;
        }
        public static async Task<WebApplication> MigrateIdentityDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbcontextService = scope.ServiceProvider.GetRequiredService<StoreIdentityDbContext>();
            var PendingmMigration = await dbcontextService.Database.GetPendingMigrationsAsync();
            if (PendingmMigration.Any())
            {
                await dbcontextService.Database.MigrateAsync();
            }
            return app;
        }


    }
}
