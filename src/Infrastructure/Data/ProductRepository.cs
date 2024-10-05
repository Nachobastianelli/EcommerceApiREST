using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context) { }

        public List<Product> LittleQuantity() 
        {
            var query = _context.Set<Product>()
                .Where(u => u.Quantity <= 20)
                .ToList();

            return query; 
        }

        public void AddQuantity (int id, int quantity)
        {
            var product = _context.Set<Product>().FirstOrDefault(p => p.Id == id) ?? throw new Exception("Not found"); 

            product.AddQuantity(quantity);

            _context.Update(product);
            _context.SaveChanges();
        }

        public List<Product> FilterByMostExpensive()
        {
            var query = _context.Set<Product>()
                .OrderByDescending(p => p.Price)
                .ToList();

            return query;
        }

        public List<Product> FilterByCheapest() 
        {
            var query = _context.Set<Product>()
                .OrderBy(p => p.Price)
                .ToList();

            return query;
        }

        public List<Product> GetByName(string name)
        {
            return _context.Set<Product>()
                .Where(u => u.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Product> ShowAvailables()
        {
            var query = _context.Set<Product>()
                .Where(p => p.IsAvailable == true)
                .ToList();

            return query;
        }
    }
}
