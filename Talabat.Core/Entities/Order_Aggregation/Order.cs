using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregation
{
    public class  Order:BaseEntity
    {

        // Must Be Paramaterless Constractor wthitout him in migragton  error 

        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAdress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal,string intentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAdress = shippingAdress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId=intentId;
        }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.Now;

        public OrderStatus Status { get; set; } = OrderStatus.pending;

        public Address ShippingAdress{ get; set; }


        //I dont need this
        //   public int DeliveryMethodId { get; set; } 
    
        public DeliveryMethod DeliveryMethod { get; set; }  //nav prop  in migration he build id there
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>(); //hashset to retrieve unique    
         public decimal SubTotal { get; set; }

        //[NotMapped]
        //public decimal Total => SubTotal + DeliveryMethod.Cost; //subtotal + deliveryMethod.Cost
        //or
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;


        public string PaymentIntentId { get; set; }   
    }
}
