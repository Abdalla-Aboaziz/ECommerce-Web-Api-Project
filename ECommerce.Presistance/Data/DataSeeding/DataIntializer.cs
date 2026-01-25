using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.ProductModules;
using ECommerce.Presistance.Data.DBContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Presistance.Data.DataSeeding
{
    public class DataIntializer : IDataInitializer
    {
        private readonly StoreDbContext _dbContext;

        public DataIntializer(StoreDbContext dbContext)
        {
          _dbContext = dbContext;
        }
        public async Task InitializeAsync()
        {
            try
            {
                // Check if he has any products or brands or types  Before
                var hasProducts =await _dbContext.Products.AnyAsync();
                var hasBrands = await _dbContext.ProductBrands.AnyAsync();
                var hasTypes = await _dbContext.ProductTypes.AnyAsync();


                if (hasProducts && hasBrands && hasTypes) return;

                if (!hasBrands) 
                {
                    await SeedDataFromJson<ProductBrand,int>("brands.json", _dbContext.ProductBrands);
                }
                if (!hasTypes)
                {
                    await SeedDataFromJson<ProductType,int>("types.json", _dbContext.ProductTypes);
                    await _dbContext.SaveChangesAsync();
                }
                if (!hasProducts)
                {
                    await SeedDataFromJson<Product,int>("products.json", _dbContext.Products);
                    await _dbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error During Data Seeding: {ex}");


            }
        }

        #region Helper Method
   private async Task SeedDataFromJson<T,TKey>(string fileName,DbSet<T> dbset) where T : BaseEnitiy<TKey>
        {
       // D:\00 - Projects\API\ECommerce\ECommerce.Presistance\Data\DataSeeding\JSONFiles

        var FilePath = @"..\ECommerce.Presistance\Data\DataSeeding\JSONFiles\" + fileName;

            if (!File.Exists(FilePath)) throw new FileNotFoundException($"Json File Not Found at Path: {FilePath}");

            try
            {
                using var DataStream = File.OpenRead(FilePath);

                var Data=await JsonSerializer.DeserializeAsync<List<T>>(DataStream,new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive=true
                });
                if (Data != null && Data.Count > 0)
                {
                  await  dbset.AddRangeAsync(Data);
                   // _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Reading json File{ex}");
               
            }
        }
        #endregion


    }
}
