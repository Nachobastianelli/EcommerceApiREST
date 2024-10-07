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
        Order Create (Order order);
        void Delete (int id);
        void Update (int id,Order order);
    }
}
