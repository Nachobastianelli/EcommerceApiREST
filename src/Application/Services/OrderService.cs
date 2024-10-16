using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public void AddToCart(string userId, int productId)
        {
            var ordersInStateNew = _orderRepository.GetOrderInStateNew(int.Parse(userId));
            var product = _productRepository.GetById(productId) ?? throw new NotFoundException($"The product whit the Id {productId} not found");
            if (ordersInStateNew == null)
            {

                Order newOrder = new Order
                {
                    IdUser = int.Parse(userId),
                };

                
                OrderLines line = new OrderLines
                {
                    Product = product,
                    Order = newOrder,
                    Quantity = 1,
                };


                newOrder.AddOrderLine(line);
                _orderRepository.Add(newOrder);
            }
            else 
            {
                _orderRepository.CancelDuplicateOrdersInStateNew(int.Parse(userId));

                var existingOrder = ordersInStateNew.FirstOrDefault();

                if (existingOrder != null)
                {   
                    var existingOrderLine = existingOrder
                                            .OrderLines
                                            .FirstOrDefault(m => m.Name == product.Name);

                    if (existingOrderLine != null)
                    {
                        existingOrderLine.Quantity += product.Quantity;
                    }
                    else
                    {
                        OrderLines newLine = new OrderLines
                        {
                            Product = product,
                            Order = existingOrder,
                            Quantity = 1,
                            OrderId = existingOrder.Id,
                            ProductId = product.Id,
                        
                        };
                        existingOrder.AddOrderLine(newLine); 
                    }

                    _orderRepository.Update(existingOrder);

                }
            }
        }

        public Order Create(string userId, Order order)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveToCart(string userId, int productId)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Order order)
        {
            throw new NotImplementedException();
        }
    }
}
