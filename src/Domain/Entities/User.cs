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
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The name must be complete")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "The name must be between 2 and 20 characters long")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "The surname must be complete")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "The name must be between 2 and 20 characters long")]
        public required string Surname { get; set; }
        public  string Fullname => Name + " " + Surname;
        [Required(ErrorMessage = "The email must be completed")]
        [EmailAddress(ErrorMessage = "Must be a valid email address")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "The password must be completed")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "The password must be at least 8 characters long")]
        [DataType(DataType.Password)]
        public  required string Password { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Roles Role {  get; set; }
        public List<Valoration> Valorations { get; set; } = new List<Valoration>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();

        public User() { }

       

    }
}
