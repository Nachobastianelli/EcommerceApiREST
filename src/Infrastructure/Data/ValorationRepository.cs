using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ValorationRepository : EfRepository<Valoration>, IValorationRepository
    {
        public ValorationRepository(ApplicationContext context) :base(context) { }

        
    }
}
