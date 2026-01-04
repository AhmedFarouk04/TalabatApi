using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.Core.Specifications.Order_Spec
{
    public class OrderSpecification:BaseSpecification<Order>
    {

       
            // Get Orders For Specific User (by email)
            public OrderSpecification(string email)
                : base(o => o.BuyerEmail == email)
            {
                Includes.Add(o => o.DeliveryMethod);
                Includes.Add(o => o.Items);
             AddOrderByDescending(o => o.OrderDate);
            }

        // Get specific order by id for user
        public OrderSpecification(int id, string email)
            : base(o => o.BuyerEmail == email && o.Id == id)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
        }

    }
}
