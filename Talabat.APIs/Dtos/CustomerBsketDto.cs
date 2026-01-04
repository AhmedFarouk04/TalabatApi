using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
    public class CustomerBsketDto
    {
        [Required]
        public string Id { get; set; }

        public List<BasketItemDto> Items { get; set; }

        public string? PaymentIntentId { get; set; }// time of checkout not null  but in basket is null

        public string? ClientSecret { get; set; }// time of checkout not null but in basket is null


        public int? DeliveryMethodsId { get; set; }

        public decimal ShippingCost { get; set; }



    }
}
       