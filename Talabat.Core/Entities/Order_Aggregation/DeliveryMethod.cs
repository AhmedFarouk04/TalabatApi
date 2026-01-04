using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregation
{

    //DM=>order   1-m
    public  class DeliveryMethod:BaseEntity
    {

        public DeliveryMethod()//empty to entityframework use it
        {
            
        }
        public DeliveryMethod(string shortName, string description, decimal cost, string deliveryTime)
        {
            ShortName = shortName;
            Description = description;
            Cost = cost;
            DeliveryTime = deliveryTime;
        }

        public  string ShortName { get; set; }



        public string Description { get; set; }
        
        public decimal Cost { get; set; }



        public string DeliveryTime { get; set; }

        //one by convension 
        //  i dont need write order here he understand oneo to one but in sql he understand it one to many

    }
}
