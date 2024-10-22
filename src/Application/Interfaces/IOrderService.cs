using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Order GetById(int id, string userId);
        List<Order> GetAll();
        List<Order> GetAllForUser(string userId);
        void UpdateOrderToStatePending (AddressDto address, string userId);
        void CancelOrder(int orderId,string userId);
        void ConfirmOrder(int orderId, string userId);
        void DeleteAllOrderLines(string userId);    

        void AddToCart(string userId, int productId);
        void RemoveToCart(string userId, int productId);
    }
}
