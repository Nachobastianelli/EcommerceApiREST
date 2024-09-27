using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private int _quantity;

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
        [MaxLength(500, ErrorMessage = "The description cannot be more than 500 characters long.")]
        public string? Description { get; set; }
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
        public Sizes Size { get; set; } //Actualizar db cambio de nombre
        public Colors Color { get; set; }
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Quantity must be greater than 0.");
                _quantity = value;
            }
        }
        public List<OrderLines> OrderLines { get; set; } = new List<OrderLines>();
        public List<Valoration> Valorations { get; set; } = new List<Valoration>();

        public Product() { }    

        public Product (string name, string? description, Category category, decimal price, string imagePath, Sizes size, Colors color, int quantity)
        {
            Name = name;
            Description = description;
            Category = category;
            Price = price;
            ImagePath = imagePath;
            Size = size;
            Color = color;
            Quantity = quantity;
            VerifyAvailable();
        }

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
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than 0.");
            Quantity += quantity;
            VerifyAvailable ();
        }


    }
}
