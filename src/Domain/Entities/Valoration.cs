using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Valoration
    {
        public int ValorationId { get; set; }
        public int IdProduct { get; set; }
        public int IdUser { get; set; }
        public Stars Stars { get; set; }
        public string? Opinion { get; set; }
    }
}
