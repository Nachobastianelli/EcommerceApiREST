using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        void Update(int id, ProductUpdateRequest product);
        void AddQuantity(int id, int quantity);
        void Delete (int id);
        List<Product> GetAll();
        List<Product> GetByName(string name);
        List<Product> LittleQuantity();
        List<Product> FilterByCheapest();
        List<Product> FilterByMostExpensive();
        List<Product> ShowAvailable();
        Product GetById(int id);
        Product Create(ProductCreateRequest productCreateRequest);
        Product GetProductByIdWithValorations(int id);





    }
}
