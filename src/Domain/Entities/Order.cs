using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderLines> OrderLines {  get; set; } = new List<OrderLines>();
        public int IdUser { get; set; }
        public User User { get; set; }
        public Invoice Invoice { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmmount { get; set; }
        public Address Address { get; set; } = new Address();
        public StateOrder StateOrder { get; set; }

        public Order() { }

        public void CalculateTotalAmmount ()
        {
            TotalAmmount = OrderLines.Sum(line => line.Total);

        }

        public void GoToCheckout()
        {
            if (StateOrder == StateOrder.New)
            {
                StateOrder = StateOrder.Pending;
            }
        }

        public void CompleteOrder()
        {
            if (StateOrder == StateOrder.Pending)
            {
                StateOrder = StateOrder.Finished;
            }
        }

        public void CanellOrder()
        {
            if (StateOrder == StateOrder.Pending)
            {
                StateOrder = StateOrder.Cancelled;
            }
        }
    }
}
