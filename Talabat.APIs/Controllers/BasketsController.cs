using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.APIs.Controllers
{

    public class BasketsController : ApiBaseController
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketsController(IBasketRepository basketRepository,IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }

        // GET: api/baskets/{id}
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);

            return basket is null
                ? new CustomerBasket(id)
                : basket;
        }

        // POST: api/baskets
        [HttpPost]
        public async Task<ActionResult<CustomerBsketDto>> UpdateBasket(CustomerBsketDto basket)
        {
            var mappedBascket=mapper.Map<CustomerBsketDto, CustomerBasket>(basket);
            var createdOrUpdatedBasket =
                await basketRepository.UpdateBasketAsync(mappedBascket);

            if (createdOrUpdatedBasket is null)
                return BadRequest(new ApiErrorResponse(400));

            return Ok(createdOrUpdatedBasket);
        }

        // DELETE: api/baskets/{id}
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            return await basketRepository.DeleteBasketAsync(id);
        }
    }
}
