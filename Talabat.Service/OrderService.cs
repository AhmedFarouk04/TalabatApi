using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregation;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.Specifications.Order_Spec;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {

        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(
        IBasketRepository basketRepository,
        IUnitOfWork UnitOfWork,IPaymentService paymentService)
        {
            this.basketRepository = basketRepository;
            _unitOfWork = UnitOfWork;
            this._paymentService = paymentService;
        }


        public async Task<Order?> CreateOrderAsync(
          string buyerEmail,
          string basketId,
          Address shippingAddress,
          int deliveryMethodId
          )
        {
            // 1. Get Basket From Basket Repo
            var basket = await basketRepository.GetBasketAsync(basketId);

            // 2. Get Selected Items at Basket From ProductRepo
            var orderItems = new List<OrderItem>();

            if (basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    if (product == null)
                        throw new Exception($"Product with Id {item.Id} not found");

                    var productItemOrdered = new ProductOrderItem(
                        product.Id,
                        product.Name,
                        product.PictureUrl
                    );

                    var orderItem = new OrderItem(
                        productItemOrdered,
                        product.Price,   
                        item.Quantity
                    );

                    orderItems.Add(orderItem);
                }
            }

            // 3. Calculate SubTotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            // 4. Get Delivery Method From DM Repository
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>()
                                      .GetByIdAsync(deliveryMethodId);

            if (deliveryMethod == null)
                throw new Exception($"DeliveryMethod with Id {deliveryMethodId} not found");


            // 5. Create Order

            //  Check If Order Exists With Same PaymentIntent (Stripe Safety)
            var spec = new OrderWithPaymentIntentSpecification(basket.PaymentIntentId);

            var existOrder = await _unitOfWork
                .Repository<Order>()
                .GetByIdWitSpecAsync(spec);

            if (existOrder != null)
            {
                _unitOfWork.Repository<Order>().Delete(existOrder);

                // Re-sync payment intent amount in case basket was updated
                await _paymentService.CreateOrUpdatePaymentIntent(basket.Id);
            }



            var order = new Order(
                buyerEmail,
                shippingAddress,
                deliveryMethod,
                orderItems,
                subTotal,basket.PaymentIntentId);


            // 6. Add Order Locally
            await _unitOfWork.Repository<Order>().Add(order);

            // 7. Save Order To DataBase (Orders)

           var result= await _unitOfWork.Complete();
            if (result <= 0) return null;
           
            return order;
        }


       

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);

            var orders = await _unitOfWork
                .Repository<Order>()
                .GetAllWitSpecAsync(spec) ;

            return orders;
        }

        public async Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var spec = new OrderSpecification(orderId, buyerEmail);

            var order = await _unitOfWork
                .Repository<Order>()
                .GetByIdWitSpecAsync(spec);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethod= await _unitOfWork
                .Repository<DeliveryMethod>()
                .GetAllAsync();


            return DeliveryMethod;
        }

    }
}
