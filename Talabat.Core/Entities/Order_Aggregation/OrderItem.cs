using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregation
{
    //OI=>POI  1TO 1 TOTAL 
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductOrderItem itemOrdered, decimal price, int quantity)
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        }

        public ProductOrderItem ItemOrdered { get; set; }  //need configration to cut it 


        public decimal Price { get; set; }

        public int Quantity { get; set; }


    }
}
