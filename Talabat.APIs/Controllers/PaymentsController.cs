using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Services;
using Talabat.Service;

namespace Talabat.APIs.Controllers
{
    public class PaymentsController : ApiBaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper mapper;
        private readonly ILogger<PaymentsController> logger;
        private readonly IConfiguration configuration;

        public PaymentsController(
    IPaymentService paymentService,
    IMapper mapper,
    ILogger<PaymentsController> logger,IConfiguration _configuration)
        {
            _paymentService = paymentService;
            this.mapper = mapper;
            this.logger = logger;
            configuration = _configuration;
        }

        [Authorize]
        [ProducesResponseType(typeof(CustomerBsketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("{basketId}")] // POST: /api/payments?id=basketId
        public async Task<ActionResult<CustomerBsketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket == null)
                return BadRequest(new ApiErrorResponse(400, "A problem with your basket"));

            return Ok(basket);
        }


        [HttpPost("webhook")]//fregement  /api/payments/webhook
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                configuration["StripeSettings:WebhookSecret"]
            );

            var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;

            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentSucceeded:
                    await _paymentService
                        .UpdatePaymentIntentToSucceededOrFaild(paymentIntent.Id, true);

                    logger.LogInformation(
                        "Payment is Succeeded ya Hamada {PaymentIntentId}",
                        paymentIntent.Id
                    );
                    break;

                case EventTypes.PaymentIntentPaymentFailed:
                    await _paymentService
                        .UpdatePaymentIntentToSucceededOrFaild(paymentIntent.Id, false);

                    logger.LogInformation(
                        "Payment is Failed ya Hamada :( {PaymentIntentId}",
                        paymentIntent.Id
                    );
                    break;
            }




            return Ok();
        }

    }
}
