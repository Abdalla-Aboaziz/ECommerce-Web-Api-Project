using ECommerce.Domain.Contracts;
using ECommerce.Presistance.Data.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Extentions
{
    public static class WebApplicationRegistration
    {
        public static WebApplication MigrateDatabase (this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbcontextService = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            if (dbcontextService.Database.GetPendingMigrations().Any())
            {
                dbcontextService.Database.Migrate();
            }
           return app;
        }

        public static WebApplication SeedData (this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dataInitializer = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
            dataInitializer.Initialize();
            return app;
        }
    }
}
