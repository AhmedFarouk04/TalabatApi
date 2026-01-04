using Microsoft.Extensions.Configuration;
using Stripe;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregation;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.Specifications.Order_Spec;
using Product = Talabat.Core.Entities.Product;
namespace Talabat.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(
            IConfiguration configuration,
            IBasketRepository basketRepository,
            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey =
                _configuration["StripeSettings:SecretKey"];

            // 1️ Get Basket
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket == null) return null;

            // 2️ Calculate Shipping Price
            var shippingPrice = 0m;
            if (basket.DeliveryMethodsId.HasValue)
            {
                var deliveryMethod = await _unitOfWork
                    .Repository<DeliveryMethod>()
                    .GetByIdAsync(basket.DeliveryMethodsId.Value);

                shippingPrice = deliveryMethod.Cost;
                basket.ShippingCost = deliveryMethod.Cost;
            }

            // 3️ Recalculate Product Prices (Security)
            if (basket.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork
                        .Repository<Product>()
                        .GetByIdAsync(item.Id);

                    if (item.Price != product.Price)
                        item.Price = product.Price;
                }
            }

            var paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent;

            // 4️ Create OR Update PaymentIntent
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)
                        basket.Items.Sum(item => item.Price * item.Quantity*100) + (long) shippingPrice * 100,//100 for dolar dolar =100 cent
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)
                        basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)shippingPrice * 100,//100 for dolar dolar =100 cent

                };

                await paymentIntentService
                    .UpdateAsync(basket.PaymentIntentId, options);
            }

            // 5 Update Basket
            await _basketRepository.UpdateBasketAsync(basket);

            return basket;
        }

        public async Task<Order> UpdatePaymentIntentToSucceededOrFaild(string IntentId, bool isSucceeded)
        {
            var spec = new OrderWithPaymentIntentSpecification(IntentId);

            var order = await _unitOfWork.Repository<Order>()
                .GetByIdWitSpecAsync(spec);

            if (isSucceeded)
                order.Status = OrderStatus.PaymentReceieved;
            else
                order.Status = OrderStatus.PaymentFailed;

            _unitOfWork.Repository<Order>().Update(order);

            await _unitOfWork.Complete();

            return order;
        }



    }
}
