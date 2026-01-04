using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregation
{
    public  enum  OrderStatus
    {
        //int
        [EnumMember(Value = "pending")]//Store in Db not int
       pending,
        [EnumMember(Value = "PaymentReceieved")]

        PaymentReceieved,
        
        [EnumMember(Value = "PaymentFailed")]


        PaymentFailed

    }
}
