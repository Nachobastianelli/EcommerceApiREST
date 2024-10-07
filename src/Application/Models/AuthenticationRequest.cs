using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AuthenticationRequest
    {
        [Required]
        [EmailAddress]
        [StringLength(100, MinimumLength = 5)]
        public string? Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string? Password { get; set; }
    }
}
