using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public List<Order> GetOrderInStateNew(int userId);
        public List<Order> GetOrderInStatePending(int userId);
        public void CancelDuplicateOrdersInStateNew(int userId);

    }
}
