using ECommerce.Shared.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ServiceAbstraction
{
    public interface IBasketServices
    {
        Task<BasketDtos> GetBasketAsync(string Id);
        Task<BasketDtos> CreateOrUpdateBasketAsync(BasketDtos basket);
        Task<bool> DeleteBasketAsync(string Id);

    }
}
