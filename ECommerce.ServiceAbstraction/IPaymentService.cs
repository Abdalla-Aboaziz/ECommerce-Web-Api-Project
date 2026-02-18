using ECommerce.Shared.BasketDtos;
using ECommerce.Shared.CommonResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ServiceAbstraction
{
    public interface IPaymentService
    {
        Task<Result<BasketDtos>> CreatePaymentIntentAsync(string BasketId);

    }
}
