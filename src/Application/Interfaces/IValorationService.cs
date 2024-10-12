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
        Valoration Create(int productID, ValorationCreateRequest valoration, string userId);
        void Update(int id, ValorationUpdateRequest valoration, string userId);
        void Delete(int id,string userId);
    }
}
