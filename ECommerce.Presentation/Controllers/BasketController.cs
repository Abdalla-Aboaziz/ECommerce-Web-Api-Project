using ECommerce.ServiceAbstraction;
using ECommerce.Shared.BasketDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController:ControllerBase
    {
        private readonly IBasketServices _basketServices;

        public BasketController(IBasketServices basketServices)
        {
            _basketServices = basketServices;
        }

        // GET api/basket?id

        [HttpGet]
        public async Task<ActionResult<BasketDtos>> GetBasket(string id)
        {
            var basket = await _basketServices.GetBasketAsync(id);
            if (basket == null)
            {
                return NotFound();
            }
            return Ok(basket);
        }

        // POST api/basket
        [HttpPost]
        public async Task<ActionResult<BasketDtos>> CreateOrUpdateBasket(BasketDtos basket)
        {
            var Basket = await _basketServices.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }

        // DELETE api/basket/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            var result = await _basketServices.DeleteBasketAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
