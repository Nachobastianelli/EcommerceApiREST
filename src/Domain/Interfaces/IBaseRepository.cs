using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T? GetById(int id);
        List<T> GetAll();
        void Update(T entity);
        void Delete(T entity);
        T Add(T entity);
    }
}
