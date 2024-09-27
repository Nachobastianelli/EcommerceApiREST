using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Valoration
    {
        public int ValorationId { get; set; }
        public int IdProduct { get; set; }
        public Product Product { get; set; }
        public int IdUser { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Stars Stars { get; set; }
        [MaxLength(300, ErrorMessage = "the opinion must be less than 300 characters")]
        public string? Opinion { get; set; }

        public Valoration() { }

        public Valoration(int productID,int userID, Stars stars, string? opinion)
        {
            IdProduct = productID;
            IdUser = userID;
            Stars = stars;
            Opinion = opinion;
        }
    }
}
