using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderLines
    {
        
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string Name => Product.Name;
        public int Quantity { get; set; }
        public decimal UnitPrice => Product.Price;
        public decimal Total => UnitPrice * Quantity;

        public OrderLines() { }

        public OrderLines(int productId, int orderId,int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
