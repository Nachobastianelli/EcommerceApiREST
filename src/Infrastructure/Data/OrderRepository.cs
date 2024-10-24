using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class OrderRepository : EfRepository<Order>  , IOrderRepository
    {

        public OrderRepository(ApplicationContext context) : base(context) { }

        public override List<Order> GetAll()
        {
            return _context.Orders
                .Include(o => o.OrderLines)
                .Include(a => a.Address)
                .Include(i => i.Invoice)
                .ToList();
        }

        public override Order? GetById(int id)
        {
            return _context.Orders
                .Include(o => o.OrderLines)
                .Include(a => a.Address)
                .Include(i => i.Invoice)
                .FirstOrDefault(i => i.Id.Equals(id));
        }

        public List<Order> GetOrderInStateNew(int userId)
        {
            var query = _context.Set<Order>()
                        .Where(u => u.IdUser == userId && u.StateOrder == Domain.Enums.StateOrder.New)
                        .Include(o => o.OrderLines)
                        .ToList();
                        

            return query;
        }

        public List<Order> GetOrderInStatePending(int userId)
        {
            var query = _context.Set<Order>()
                        .Where(u => u.IdUser == userId && u.StateOrder == Domain.Enums.StateOrder.Pending)
                        .ToList();

            return query;
        }

        public void CancelDuplicateOrdersInStateNew(int userId)
        {

            var ordersInStateNew = GetOrderInStateNew(userId);

            
            if (ordersInStateNew.Count > 1)
            {
                
                foreach (var order in ordersInStateNew.Skip(1)) 
                {
                    order.StateOrder = Domain.Enums.StateOrder.Cancelled;
                    _context.Update(order);
                }
                _context.SaveChanges();

                
            }
        }
    }
}
