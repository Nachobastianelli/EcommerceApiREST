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
    }
}
