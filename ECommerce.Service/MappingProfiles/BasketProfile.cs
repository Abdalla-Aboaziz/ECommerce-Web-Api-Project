using AutoMapper;
using ECommerce.Domain.Entities.BasketModules;
using ECommerce.Shared.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.MappingProfiles
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap <CustomerBasket, BasketDtos>().ReverseMap();
            CreateMap<BasketItem, BasketItemDtos>().ReverseMap();

        }
    }
}
