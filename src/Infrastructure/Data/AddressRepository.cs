using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AddressRepository : EfRepository<Address> , IAddressRepository
    {
        public AddressRepository(ApplicationContext context) : base(context) { }


        public Address GetAddressOrder(int orderId)
        {
            var query = _context.Set<Address>()
                .Where(a => a.OrderId == orderId)
                .FirstOrDefault();

            return query;
        } 
    }
}
