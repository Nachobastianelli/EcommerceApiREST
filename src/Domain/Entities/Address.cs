using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Address
    {
        public int Id { get; set; } 
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string Street { get; set; }
        public string City { get; set; } = "Rosario";
        public string State { get; set; } = "Santa Fe";
        public string PostalCode { get; set; } = "2000";
        public string Country { get; set; } = "Argentina";
        public string Phone { get; set; }

        public Address() { }

    }
}
