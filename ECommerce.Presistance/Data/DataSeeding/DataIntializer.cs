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
        public void Initialize()
        {
            try
            {
                // Check if he has any products or brands or types  Before
                var hasProducts = _dbContext.Products.Any();
                var hasBrands = _dbContext.ProductBrands.Any();
                var hasTypes = _dbContext.ProductTypes.Any();


                if (hasProducts && hasBrands && hasTypes) return;

                if (!hasBrands) 
                {
                SeedDataFromJson<ProductBrand,int>("brands.json", _dbContext.ProductBrands);
                }
                if (!hasTypes)
                { 
                SeedDataFromJson<ProductType,int>("types.json", _dbContext.ProductTypes);
                     _dbContext.SaveChanges();
                }
                if (!hasProducts)
                { 
                SeedDataFromJson<Product,int>("products.json", _dbContext.Products);
                    _dbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error During Data Seeding: {ex}");


            }
        }

        #region Helper Method
   private void SeedDataFromJson<T,TKey>(string fileName,DbSet<T> dbset) where T : BaseEnitiy<TKey>
        {
            //D:\00-Projects\API\ECommerce\ECommerce.Presistance\Data\DataSeeding\JSONFiles\

            var FilePath = @"..ECommerce.Presistance\Data\DataSeeding\JSONFiles\" + fileName;

            if (!File.Exists(FilePath)) throw new FileNotFoundException($"Json File Not Found at Path: {FilePath}");

            try
            {
                using var DataStream = File.OpenRead(FilePath);

                var Data=JsonSerializer.Deserialize<List<T>>(DataStream,new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive=true
                });
                if (Data != null && Data.Count > 0)
                {
                    dbset.AddRange(Data);
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
