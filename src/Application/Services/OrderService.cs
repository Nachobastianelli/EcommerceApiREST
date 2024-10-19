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

        private bool productIsAvailable(Product product)
        {
            product.VerifyAvailable();
            return product.IsAvailable && product.Quantity > 0;
        }

        public void AddToCart(string userId, int productId)
        {
            var product = _productRepository.GetById(productId) ?? throw new NotFoundException($"The product whit the Id {productId} not found");
            
            if (!productIsAvailable(product)) throw new OutOfStockException($"The product with the Id {productId} is out of stock.");

            var ordersInStateNew = _orderRepository.GetOrderInStateNew(int.Parse(userId));
            if (ordersInStateNew.Count == 0)
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
                newOrder.OrderLines.Add(line);
                product.AddQuantity(-1);
                _productRepository.Update(product);
                _orderRepository.Add(newOrder);
            }
            else 
            {
                if (ordersInStateNew.Count > 1)
                {
                    _orderRepository.CancelDuplicateOrdersInStateNew(int.Parse(userId));
                }

                var existingOrder = ordersInStateNew.FirstOrDefault();

                if (existingOrder != null)
                {   
                    var existingOrderLine = existingOrder
                                            .OrderLines
                                            .FirstOrDefault(m => m.Name == product.Name);

                    if (existingOrderLine != null)
                    {
                        existingOrderLine.Quantity += 1;
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

                    product.AddQuantity(-1);
                    _productRepository.Update(product);
                    _orderRepository.Update(existingOrder);

                }
            }
        }

        public void RemoveToCart(string userId, int productId)
        {
            var checkOrder = _orderRepository.GetOrderInStateNew(int.Parse(userId))?? throw new NotFoundException("The user does not have an order");

            if (checkOrder.Count > 1)
                _orderRepository.CancelDuplicateOrdersInStateNew(int.Parse(userId));

            var product = _productRepository.GetById(productId) ?? throw new NotFoundException($"The product with the Id {productId} not found");

            var exisitingOrder = checkOrder.FirstOrDefault();

            OrderLines existingOrderLine = exisitingOrder.OrderLines.FirstOrDefault(o => o.Name == product.Name) ?? throw new NotFoundException("you dont have this product in your order");

            exisitingOrder.RemoveOrderLine(existingOrderLine);

            product.AddQuantity(1);
            
            _productRepository.Update(product);
            _orderRepository.Update(exisitingOrder);
            
        }

        //elimine el create y el delete por ahora por que pienso que no se deberian de utilizar...

        public List<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public Order GetById(int id)
        {
            return _orderRepository.GetById(id) ?? throw new NotFoundException($"Order with id {id} not found");
        }


        public void Update(int orderId,Address? address, string userId)
        {
            var order = GetById(orderId);

            var userID = int.Parse(userId);

            if (order.IdUser == userID)
            {
                if (address != null && order.Address != address) order.Address = address;
                _orderRepository.Update(order);
            }
            else
            {
                throw new ArgumentException("you cannot modify an order that is not yours.");
            }

        }
    }
}
