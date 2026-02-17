using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.OrderDtos
{
    public class OrderToReturnDto
    {
        public Guid Id { get; init; }
        public string UserEmail { get; init; } = string.Empty;
        public ICollection<OrderItemDto> Items { get; init; } = new List<OrderItemDto>();
        public AddressDto Address { get; init; } = null!;
        public string DeliveryMethod { get; init; } = string.Empty;
        public string OrderStatus { get; init; } = string.Empty;
        public DateTimeOffset OrderDate { get; init; }
        public decimal SubTotal { get; init; }
        public decimal Total { get; init; }
    }
}
