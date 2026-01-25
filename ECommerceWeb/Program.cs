
using ECommerce.Domain.Contracts;
using ECommerce.Presistance.Data.DataSeeding;
using ECommerce.Presistance.Data.DBContexts;
using ECommerce.Presistance.Repository;
using ECommerce.Service;
using ECommerce.Service.MappingProfiles;
using ECommerce.ServiceAbstraction;
using ECommerceWeb.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

            #region DataBaseConfigration
            builder.Services.AddDbContext<StoreDbContext>(opt =>
                {
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                }); 
            #endregion
            #region Service Registeration
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            //builder.Services.AddAutoMapper(x=>x.AddProfile(typeof(ProductProfile)));
            //builder.Services.AddTransient<ProductPictureResolver>(); // Register the resolver
            builder.Services.AddAutoMapper(typeof(ServiceAssemplyRefrence).Assembly);
            builder.Services.AddScoped<IProductServices,ProductService>();
            builder.Services.AddScoped<IDataInitializer,DataIntializer>();

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
            app.UseStaticFiles(); // Enable serving static files

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
