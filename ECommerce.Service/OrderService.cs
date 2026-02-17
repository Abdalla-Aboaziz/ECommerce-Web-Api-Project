using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.BasketModules;
using ECommerce.Domain.Entities.OrderModules;
using ECommerce.Domain.Entities.ProductModules;
using ECommerce.ServiceAbstraction;
using ECommerce.Shared.CommonResult;
using ECommerce.Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IMapper mapper,IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
           _mapper = mapper;
           _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string Email)
        {
            // Map Addres 
            var OrderAddres = _mapper.Map<OrderAddress>(orderDto.Address);
            var Basket = await _basketRepository.GetBasketAsync(orderDto.BasketId);
            if (Basket is null) return Error.NotFound("Basket not found",$"Basket with id {orderDto.BasketId} Not Found ");

            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var item in Basket.Items)
            {
                var Proudct = await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(item.Id);
                if (Proudct is null) return Error.NotFound("Product not found", $"Product with id {item.Id} Not Found ");
                 
                orderItems.Add(CreateOrderItem(item, Proudct));

            }
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId);
            if ( DeliveryMethod is null ) return Error.NotFound("Delivery method not found", $"Delivery method with id {orderDto.DeliveryMethodId} Not Found ");

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            var order = new Order
            {
                Address = OrderAddres,
                DeliveryMethod = DeliveryMethod,
                Items = orderItems,
                Subtotal = subtotal,
                UserEmail = Email
            };
            await _unitOfWork.GetRepository<Order,Guid>().AddAsync(order);
           int result = await _unitOfWork.SaveChangesAsync();
            if (result==0) return Error.Failure("Failed to create order", "An error occurred while creating the order. Please try again.");
            return _mapper.Map<OrderToReturnDto>(order);


        }

        #region Helper Method
        private static OrderItem CreateOrderItem(BasketItem item, Product product)
        {
            return new OrderItem
            {
                Product = new ProductItemOrder
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    PictureUrl = product.PictureUrl
                },
                Price = product.Price,
                Quantity = item.Quantity
            };
        } 
        #endregion
    }
}
