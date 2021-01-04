using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using EventBusRabbitMQ.Common;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;
        private readonly EventBusRabbitMqProducer _eventBus;
        public BasketController(IBasketRepository repository, IMapper mapper, EventBusRabbitMqProducer eventBus)
        {
            _repository = repository;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> GetBasket(string userName)
        {
            var basket =await _repository.GetBasket(userName);
            return Ok(basket??new BasketCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BasketCart),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> UpdateBasket([FromBody] BasketCart basket)
        {
            return Ok(await _repository.UpdateBasket(basket));
        }

        [HttpDelete("{userName}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> UpdateBasket(string userName)
        {
            return Ok(await _repository.DeleteBasket(userName));
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BasketCart>> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            // get total price of basket
            //remove the basket
            // send checkout event to rabbitmq

            var basket = await _repository.GetBasket(basketCheckout.UserName);
            if (basket == null)
            {
                return BadRequest();
            }

            var basketRemoved = await _repository.DeleteBasket(basket.UserName);
            if (!basketRemoved)
            {
                return BadRequest();
            }

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.RequestId = Guid.NewGuid();
            eventMessage.TotalPrice = basket.TotalPrice;

            try
            {
                _eventBus.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, eventMessage);
            }
            catch (Exception)
            {
                throw;
            }

            return Accepted();
        }

    }
}
