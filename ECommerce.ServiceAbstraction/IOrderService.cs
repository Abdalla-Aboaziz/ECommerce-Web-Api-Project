using ECommerce.Shared.CommonResult;
using ECommerce.Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ServiceAbstraction
{
    public interface IOrderService
    {
        // Create Order
        // OrderDto ,Email=>Order To Return Dto 
        Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string Email);
    }
}
