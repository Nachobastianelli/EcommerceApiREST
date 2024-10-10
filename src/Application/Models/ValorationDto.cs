using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ValorationCreateRequest
    {
        [Required]
        public Stars Stars { get; set; }
        
        [StringLength(300, MinimumLength = 0, ErrorMessage = "The description must be between 0 and 300 characters long")]
        public string? Opinion { get; set;}
    }

    public class ValorationUpdateRequest
    {
        public Stars? Stars { get; set; }
        [StringLength(300, MinimumLength = 0, ErrorMessage = "The description must be between 0 and 300 characters long")]

        public string? Opinion { get; set; }
    }

    public class ValorationDto
    {
        public Stars Stars { get; set; }
        public string? Opinion { get; set; }
    }
}
