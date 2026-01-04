using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Services;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.APIs.Controllers
{
    [Authorize]
    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost] // POST : /api/orders
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var address = mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);

            var order = await orderService.CreateOrderAsync(
                buyerEmail,
                orderDto.BasketId,
                address,
                orderDto.DeliveryMethodId
            );

            if (order is null)
                return BadRequest(new ApiErrorResponse(400));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var orders = await orderService.GetOrdersForUserAsync(buyerEmail);

            var mappedOrders = mapper.Map<
                IReadOnlyList<Order>,
                IReadOnlyList<OrderToReturnDto>>(orders);

            return Ok(mappedOrders);
        }


        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] // GET: api/Orders/1
        public async Task<ActionResult<Order>> GetOrderForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var order = await orderService.GetOrderByIdForUserAsync(id, buyerEmail);
            var mapperOrder = mapper.Map<Order, OrderToReturnDto>(order);

            if (mapperOrder is null)
                return NotFound(new ApiErrorResponse(404));

            return Ok(mapperOrder);
        }

        [HttpGet("deliverymethods")] // GET: /api/Orders/deliveryMethods
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethids()
        {
            var deliveryMethods = await orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }



    }
}
