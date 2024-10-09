using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IValorationService
    {
        Valoration GetById(int id);
        List<Valoration> GetAll();
        Valoration Create(int productID, ValorationCreateRequest valoration);
        void Update(int id, ValorationUpdateRequest valoration);
        void Delete(int id);
    }
}
