using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int IdOrder { get; set; }
        public Order Order { get; set; }
        public int IdUser { get; set; }
        public User User { get; set; } 
        public required string UserName {  get; set; }
        public required string OrderState { get; set; }
        public decimal TotalAmount { get; set; }

        public Invoice() { }
    }
}
