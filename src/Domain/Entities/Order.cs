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
        public List<OrderLines> OrderLines {  get; set; }
        public int IdUser { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmmount { get; set; }
        public string Addres { get; set; }
        public StateOrder StateOrder { get; set; }

        public void CalculateTotalAmmount (List<OrderLines> lines)
        {
            TotalAmmount = 0;
            foreach (OrderLines line in lines)
            {
                TotalAmmount = TotalAmmount + line.Total;
            }

        }
    }
}
