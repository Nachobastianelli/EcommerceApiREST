using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        private decimal _price;
        private string _name;

        public int Id { get; set; }
        public string Name {
            get => _name;
            set 
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Name cannot be empty.");
                _name = value;
            }
        }
        public string Description { get; set; }
        public Category Category { get; set; }

        public decimal Price {
            get => _price;
            set 
            {
                if(value <= 0)
                    throw new ArgumentException("Price must be greater than 0.");
                _price = value;
            } 
        }

        public string ImagePath { get; set; }
        public bool IsAvailable { get; set; }
        public Sizes size { get; set; }
        public Colors Color { get; set; }
        public int Quantity { get; set; }
        public List<OrderLines> OrderLines { get; set; } = new List<OrderLines>();
        public List<Valoration> Valorations { get; set; } = new List<Valoration>();

        public Product() { }    

        public void VerifyAvailable () 
        {
            IsAvailable = Quantity >= 1;
        }

        public void DecressQuantity(int quantity)
        {
            if (Quantity < quantity) 
            {
                throw new InvalidOperationException("Insufficient stock.");
            }
            else
            {
                Quantity -= quantity;
                VerifyAvailable();
            }

        }

        public void AddQuantity (int quantity)
        {
            if (quantity < 0) throw new ArgumentException("Quantity must be greater than 0.");
            Quantity += quantity;
            VerifyAvailable ();
        }


    }
}
