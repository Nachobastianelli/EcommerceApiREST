using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UserCreateRequest
    {
        [Required(ErrorMessage = "The name must be complete")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The name must be between 2 and 50 characters long")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "The surname must be complete")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The name must be between 2 and 50 characters long")]
        public required string Surname { get; set; }

        [Required(ErrorMessage = "The email must be completed")]
        [EmailAddress(ErrorMessage = "Must be a valid email address")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "The password must be completed")]
        [MinLength(8, ErrorMessage = "The password must be at least 8 characters long")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }

    public class UserUpdateRequest
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The name must be between 2 and 50 characters long")]
        public string Name { get; set; }
  
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The name must be between 2 and 50 characters long")]
        public string Surname { get; set; }
       
        [MinLength(8, ErrorMessage = "The password must be at least 8 characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
