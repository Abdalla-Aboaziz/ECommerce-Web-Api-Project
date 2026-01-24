
using ECommerce.Domain.Contracts;
using ECommerce.Presistance.Data.DBContexts;
using ECommerce.Presistance.Repository;
using ECommerce.Service;
using ECommerce.Service.MappingProfiles;
using ECommerce.ServiceAbstraction;
using ECommerceWeb.Extentions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerceWeb
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #region Service Registeration
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(x=>x.AddProfile(typeof(ProductProfile)));
            builder.Services.AddScoped<IProductServices,ProductService>();
            #endregion

            var app = builder.Build();

            #region DataSeeding
             await  app.MigrateDatabaseAsync();
            await app.SeedDataAsync();
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
