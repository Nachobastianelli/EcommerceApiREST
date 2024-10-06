using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(500, ErrorMessage = "The description cannot be more than 500 characters long.")]
        public string? Description { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public bool IsAvailable { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Sizes Size { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Colors Color { get; set; }
        public int Quantity { get; set; } //fijarse si da error al crear producto por el private set. Resolucion = saque el private pero solo se va a poder ingresar cantidad al momento de la creacion, despues se maneja por el metodo addQuanitity
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

        public void AddQuantity (int quantity)
        {
            if (quantity == 0) throw new ArgumentException("The quantity must be other than 0.");
            Quantity += quantity;
            VerifyAvailable ();
        }


    }
}
