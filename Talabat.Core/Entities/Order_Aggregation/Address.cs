using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregation
{
    // 1 to 1 total=>table order only
     
    public  class Address
    {
        public Address()//empty to entityframework use it
        {
        
        }
        public Address(string fname, string lname, string street, string city, string country)
        {
            FirstName = fname;
            LastName = lname;
            Street = street;
            City = city;
            Country = country;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Street { get; set; }
        
        public string City { get; set; }

        public string Country { get; set; }


    }
}
