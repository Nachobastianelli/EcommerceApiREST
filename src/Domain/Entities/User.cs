using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public  string Fullname => Name + Surname;
        public required string Email { get; set; }
        public  required string Password { get; set; }
        public Roles Role {  get; set; }
        public List<Order>? Orders { get; set; }
        public List<Invoice>? Invoices { get; set; }
    }
}
