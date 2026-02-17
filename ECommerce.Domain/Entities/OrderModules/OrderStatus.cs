using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities.OrderModules
{
    public enum OrderStatus
    {
        pending,
        paymentReceived,
        paymentFailed,
    }
}
