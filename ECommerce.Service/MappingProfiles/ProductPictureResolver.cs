using AutoMapper;
using ECommerce.Domain.Entities.ProductModules;
using ECommerce.Shared.ProductDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.MappingProfiles
{
    public class ProductPictureResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }
            if(source.PictureUrl.StartsWith("http"))
            {
               
                return source.PictureUrl;
            }
           var BaseURL = _configuration.GetSection("URLs")["BaseURL"];
            if(string.IsNullOrEmpty(BaseURL))
            {
                return string.Empty;
            }
            var PictureURL = $"{BaseURL}/{source.PictureUrl}";
            return PictureURL;

        }
    }
}
