using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }


        public void AddQuantity(int id, int quantity)
        {
            var product = _repository.GetById(id) ?? throw new ArgumentNullException(nameof(id));

            if (product.Quantity + quantity < 0 )
            {
                throw new InvalidOperationException("No es posible reducir el stock por debajo de cero.");
            }

            product.AddQuantity(quantity);
            product.VerifyAvailable();
            _repository.Update(product);
            

        }

        public Product Create(ProductCreateRequest productCreateRequest)
        {


            var existingProducts = _repository.GetByName(productCreateRequest.Name);
            if (existingProducts?.Any() == true)
            {
                var existingProduct = existingProducts.FirstOrDefault(p =>
                    p.Category == productCreateRequest.Category &&
                    p.Color == productCreateRequest.Color &&
                    p.Size == productCreateRequest.Size &&
                    p.Price == productCreateRequest.Price);

                if (existingProduct != null)
                {
                    throw new Exception("A product with these characteristics already exists");
                }
            }

            if (productCreateRequest.Price <= 0)
            {
                throw new InvalidPriceException("The price must be greater than 0.");
            }

            Product producto = new Product
            {
                Name = productCreateRequest.Name,
                Description = productCreateRequest.Description,
                Category = productCreateRequest.Category,
                ImagePath = productCreateRequest.ImagePath,
                Price = productCreateRequest.Price,
                Size = productCreateRequest.Size,
                Color = productCreateRequest.Color,
                Quantity = productCreateRequest.Quantity,
                IsAvailable = productCreateRequest.Quantity > 1
            };

            return _repository.Add(producto);

          

        }

        public void Delete(int id)
        {

            var product = _repository.GetById(id) ?? throw new ArgumentNullException(nameof(id));

            _repository.Delete(product);
        }

        public List<Product> FilterByCheapest()
        {
            return _repository.FilterByCheapest() ?? throw new NotFoundException(typeof(Product).ToString());
        }

        public List<Product> FilterByMostExpensive()
        {
            return _repository.FilterByMostExpensive() ?? throw new NotFoundException(typeof(Product).ToString());
        }

        public List<Product> GetAll()
        {
            return _repository.GetAll() ?? throw new NotFoundException(typeof(Product).ToString());
        }

        public Product GetById(int id)
        {
            return _repository.GetById(id) ?? throw new NotFoundException(typeof(Product).ToString());

        }

        public List<Product> GetByName(string name)
        {
            return _repository.GetByName(name) ?? throw new NotFoundException(typeof(Product).ToString());

        }

        public List<Product> LittleQuantity()
        {
            return _repository.LittleQuantity() ?? throw new NotFoundException(typeof(Product).ToString());

        }

        public List<Product> ShowAvailable()
        {
            return _repository.ShowAvailables() ?? throw new NotFoundException(typeof(Product).ToString());
        }

        public void Update(int id, ProductUpdateRequest product)
        {
            var existingProduct = _repository.GetById(id) ?? throw new NotFoundException(typeof(Product).ToString());

            if (product.Name != null) existingProduct.Name = product.Name;
            if (product.Price > 1) existingProduct.Price = product.Price;
            if (product.Category != existingProduct.Category) existingProduct.Category = product.Category;
            if (product.Description != null) existingProduct.Description = product.Description;
            if (product.ImagePath != null) existingProduct.ImagePath = product.ImagePath;
             
            _repository.Update(existingProduct);
        }
    }
}
