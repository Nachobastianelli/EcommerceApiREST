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
        Order GetById(int id);
        List<Order> GetAll();
        void Update (int id, Address address, string userId);
        void AddToCart(string userId, int productId);
        void RemoveToCart(string userId, int productId);
    }
}
