﻿using Domain.Entities;
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

        public List<Product> FilterByMostExpensive()
        {
            var query = _context.Set<Product>()
                .AsEnumerable() //Esto es para pasar los datos a memoria ya que SQLITE no soporta el tipo de dato decimal, ineficiente en tablas grandes pero lo voy a dejar hasta que lo pase a SqlServer
                .OrderByDescending(p => p.Price)
                .ToList();

            return query;
        }

        public List<Product> FilterByCheapest() 
        {
            var query = _context.Set<Product>()
                .AsEnumerable()
                .OrderBy(p => p.Price)
                .ToList();

            return query;
        }

        public List<Product> GetByName(string name)
        {
             var query = _context.Set<Product>()
                .Where(u => u.Name.Contains(name.ToLower()))
                .ToList();

            return query;
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