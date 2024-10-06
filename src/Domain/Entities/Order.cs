using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderLines> OrderLines {  get; private set; } = new List<OrderLines>();
        public int IdUser { get; set; }
        public User User { get; private set; }
        public Invoice Invoice { get; set; }
        public DateTime Date { get; private set; } = DateTime.Now;
        public decimal TotalAmmount { get; private set; }
        public required Address Address { get; set; } = new Address();
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StateOrder StateOrder { get; private set; } = StateOrder.New;

        public Order() { }

        public Order(User user, Address address)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Address = address ?? throw new ArgumentNullException(nameof(address));
            CalculateTotalAmmount();
        }


        public void CalculateTotalAmmount ()
        {
            TotalAmmount = OrderLines?.Sum(line => line.Total) ?? 0;

        }

        public void AddOrderLine (OrderLines orderLine)
        {
            if (orderLine == null)
            {
                throw new ArgumentNullException(nameof(orderLine));
            }
            if (StateOrder != StateOrder.New)
            {
                throw new InvalidOperationException("It is not possible to add products to the order if it is not new.");
            }

            OrderLines.Add(orderLine);
            CalculateTotalAmmount();
        }

        public void RemoveOrderLine(OrderLines orderLine)
        {
            if (orderLine == null)
            {
                throw new ArgumentNullException(nameof(orderLine));
            }
            if (StateOrder != StateOrder.New)
            {
                throw new InvalidOperationException("It is not possible to remove products to the order if it is not new.");
            }

            OrderLines.Remove(orderLine);
            CalculateTotalAmmount();
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

        public void CancelOrder()
        {
            if (StateOrder == StateOrder.Pending)
            {
                StateOrder = StateOrder.Cancelled;
            }
            else
            {
                throw new InvalidOperationException("Cannot cancel an order that is not pending.");
            }
        }
    }
}
