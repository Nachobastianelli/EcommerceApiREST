using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        public List<Product> FilterByCheapest();
        public List<Product> FilterByMostExpensive();
        public List<Product> LittleQuantity();
        public List<Product> GetByName(string name);
        public List<Product> ShowAvailables();


    }
}
