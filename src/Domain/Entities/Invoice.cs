using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; private set; } = DateTime.UtcNow; 
        public int IdOrder { get; set; }
        public Order Order { get; set; }
        public int IdUser { get; set; }
        public User User { get; set; }
        [MaxLength(30)]
        public required string UserName { get; set; }
        public required StateOrder OrderState { get; set; }
        public decimal TotalAmount { get; set; } 
        public string PaymentMethod { get; set; } 

        public static readonly string CompanyName = "Bastianelli S.A.";
        public static readonly string CompanyAddress = "Calle [123], Rosario, Santa Fe , Argentina";
        public static readonly string CompanyPhone = "+123 456 7890";
        public static readonly string CompanyEmail = "contacto@bastianelli.com";
        
        public Invoice() { }

       
    }
}
