using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AddressDto
    {
        public required string Street { get; set; }
        [Phone]
        public required string Phone { get; set; }
    }

    
}
