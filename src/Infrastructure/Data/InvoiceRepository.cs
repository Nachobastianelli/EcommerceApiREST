using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class InvoiceRepository : EfRepository<Invoice> , IInvoiceRepository
    {
        public InvoiceRepository(ApplicationContext context) : base(context) { }
    }
}
