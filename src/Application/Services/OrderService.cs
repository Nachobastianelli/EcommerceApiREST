using Application.Interfaces;
using Application.Models;
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
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IAddressRepository addressRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _addressRepository = addressRepository;
            _userRepository = userRepository;

        }

        private bool productIsAvailable(Product product)
        {
            product.VerifyAvailable();
            return product.IsAvailable && product.Quantity > 0;
        }

        /// <summary>
        /// Deletes all the order lines for a user's active order with the state 'New'.
        /// Restores the product quantities when removing the order lines.
        /// </summary>
        /// <param name="userId">The ID of the user whose order lines are to be deleted.</param>
        /// <exception cref="NotFoundException">Thrown when no order in state 'New' is found for the user.</exception>
        public void DeleteAllOrderLines(string userId)
        {
            var ordersInStateNew = _orderRepository.GetOrderInStateNew(int.Parse(userId)) ?? throw new NotFoundException("The user does not have an order whit state new");

            if (ordersInStateNew.Count > 1)
                _orderRepository.CancelDuplicateOrdersInStateNew(int.Parse(userId));

            var existingOrder = ordersInStateNew.FirstOrDefault();

            if (existingOrder.OrderLines.Count < 1) throw new BadRequestException("you do not yet have an order line in your order");

            if (existingOrder != null || existingOrder.OrderLines.Count >= 1) 
            {



                for (int i = existingOrder.OrderLines.Count - 1; i >= 0; i--)
                {
                    var line = existingOrder.OrderLines[i];
                    var product = _productRepository.GetById(line.ProductId);

                    if (product != null)
                    {
                        product.AddQuantity(line.Quantity); 
                        _productRepository.Update(product); 
                    }
                    if (line.Quantity > 1)
                    {
                        while (line.Quantity > 0)
                            existingOrder.RemoveOrderLine(line);
                    }
                    else
                    {
                        existingOrder.RemoveOrderLine(line);
                    }
                }

                _orderRepository.Update(existingOrder); 
            }
            else
            {
                throw new NotFoundException("The order does not have an orderLine(s)");
            }

        }

        /// <summary>
        /// Adds a product to the user's cart (an order with the state 'New').
        /// If no such order exists, a new one is created. Updates product availability accordingly.
        /// </summary>
        /// <param name="userId">The ID of the user who is adding the product to the cart.</param>
        /// <param name="productId">The ID of the product being added.</param>
        /// <exception cref="NotFoundException">Thrown when the product is not found.</exception>
        /// <exception cref="OutOfStockException">Thrown when the product is out of stock.</exception>
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
                    Name = product.Name,
                    OrderId = newOrder.Id,
                    ProductId = product.Id,
                    UnitPrice = product.Price,
                };


                newOrder.AddOrderLine(line);
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
                                            .FirstOrDefault(m => m.ProductId == product.Id);

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
                            Name = product.Name,
                            UnitPrice = product.Price,
                        };
                        
                        existingOrder.AddOrderLine(newLine); 
                    }

                    product.AddQuantity(-1);
                    existingOrder.CalculateTotalAmmount();
                    _productRepository.Update(product);
                    _orderRepository.Update(existingOrder);

                }
            }
        }

        /// <summary>
        /// Removes a product from the user's cart (an order with the state 'New').
        /// Restores the product quantity when removing the product from the order.
        /// </summary>
        /// <param name="userId">The ID of the user whose product is being removed.</param>
        /// <param name="productId">The ID of the product to remove.</param>
        /// <exception cref="NotFoundException">Thrown when the product or order is not found.</exception>
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


        /// <summary>
        /// Retrieves all orders placed by a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose orders are to be retrieved.</param>
        /// <returns>A list of orders placed by the user.</returns>
        public List<Order> GetAllForUser(string userId)
        {
            List<Order> orders = _orderRepository.GetAll()
                .Where(o => o.IdUser == int.Parse(userId))
                .ToList();

            return orders;
        }


        /// <summary>
        /// Retrieves a specific order by its ID for a given user.
        /// Ensures the order belongs to the user.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The order that matches the ID and belongs to the user.</returns>
        /// <exception cref="NotFoundException">Thrown when the order is not found.</exception>
        /// <exception cref="BadRequestException">Thrown when the order does not belong to the user.</exception>
        public Order GetById(int id, string userId)
        {
            var userID = int.Parse(userId);
            var order = _orderRepository.GetById(id) ?? throw new NotFoundException($"Order with id {id} not found");
            if (userID != order.IdUser)
                throw new BadRequestException("This order is not yours");
            return order;
        }

        /// <summary>
        /// Updates the state of a user's order to 'Pending' after verifying the address information is complete.
        /// </summary>
        /// <param name="address">The address details to update.</param>
        /// <param name="userId">The ID of the user whose order is being updated.</param>
        /// <exception cref="NotFoundException">Thrown when no address or order is found.</exception>
        /// <exception cref="BadRequestException">Thrown when the address fields are incomplete.</exception>
        public void UpdateOrderToStatePending(AddressDto address, string userId)
        {
            var newOrders = _orderRepository.GetOrderInStateNew(int.Parse(userId));

            var order = newOrders.FirstOrDefault();

            if (order.OrderLines.Count == 0) throw new BadRequestException("You must have at least 1 product in your order to update the status.");

            var existingAddress = _addressRepository.GetAddressOrder(order.Id);

            if (order.IdUser == int.Parse(userId))
            {
                existingAddress.Phone = address.Phone;
                existingAddress.Street = address.Street;
                
                _addressRepository.Update(existingAddress);

                if (existingAddress.Phone != null && existingAddress.Street != null )  
                    order.GoToCheckout();
                else
                    throw new BadRequestException("The Phone and Street fields must be filled in to change the orderState.");

                
                _orderRepository.Update(order);
            }
            else
            {
                throw new ArgumentException("you cannot modify an order that is not yours.");
            }

        }

        /// <summary>
        /// Cancels an order with the state 'Pending' for a specific user.
        /// </summary>
        /// <param name="orderId">The ID of the order to cancel.</param>
        /// <param name="userId">The ID of the user cancelling the order.</param>
        /// <exception cref="NotFoundException">Thrown when the order is not found.</exception>
        /// <exception cref="NotAllowedException">Thrown when the order does not belong to the user.</exception>
        /// <exception cref="BadRequestException">Thrown when the order is not in a 'Pending' state.</exception>
        public void CancelOrder(int orderId, string userId)
        {
            var order = _orderRepository.GetById(orderId) ?? throw new NotFoundException($"The order whit the id {orderId} not found");

            if (order.IdUser != int.Parse(userId)) throw new NotAllowedException("This order is not yours");

            if (order.StateOrder != Domain.Enums.StateOrder.Pending) throw new BadRequestException($"The order state must be pending. Your OrderState: {order.StateOrder}");

            for (int i = order.OrderLines.Count - 1; i >= 0; i--)
            {
                var line = order.OrderLines[i];
                var product = _productRepository.GetById(line.ProductId);

                if (product != null)
                {
                    product.AddQuantity(line.Quantity);
                    _productRepository.Update(product);
                }

            }

            order.CancelOrder();
            _orderRepository.Update(order);
        }



        /// <summary>
        /// Confirms the order with the state 'Pending' for a specific user, completing the purchase process.
        /// </summary>
        /// <param name="orderId">The ID of the order to confirm.</param>
        /// <param name="userId">The ID of the user confirming the order.</param>
        /// <exception cref="NotFoundException">Thrown when the order is not found.</exception>
        /// <exception cref="NotAllowedException">Thrown when the order does not belong to the user.</exception>
        /// <exception cref="BadRequestException">Thrown when the order is not in a 'Pending' state.</exception>
        public void ConfirmOrder(int orderId, string userId)
        {
            var order = _orderRepository.GetById(orderId) ?? throw new NotFoundException($"The order whit the id {orderId} not found");
            
            if (order.IdUser != int.Parse(userId)) throw new NotAllowedException("This order is not yours");

            if (order.StateOrder != Domain.Enums.StateOrder.Pending) throw new BadRequestException($"The order state must be pending. Your OrderState: {order.StateOrder}");

            var user = _userRepository.GetById(int.Parse(userId));

            order.CompleteOrder();

            var invoice = new Invoice
            {
                UserName = user.Fullname,
                IdUser = user.Id,
                IdOrder = orderId,
                OrderState = order.StateOrder,
                PaymentMethod = "Credit card",
                TotalAmount = order.TotalAmmount,
            };

            order.AddInvoiceToOrder(invoice);

            _orderRepository.Update(order);
        }
    }
}
