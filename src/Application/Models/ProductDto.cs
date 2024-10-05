using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProductCreateRequest
    {
        [Required]
        [StringLength(30,MinimumLength = 3, ErrorMessage = "The name must be between 2 and 50 characters long")]
        public string Name { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The description must be under to 50 characters long")]
        public string Description { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ImagePath { get; set; }
        [Required]
        public Sizes Size { get; set; }
        [Required]
        public Colors Color { get; set; }
        [Required]
        public int Quantity { get; set; }

    }

    public class ProductUpdateRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public string? ImagePath { get; set; }
    }
}
