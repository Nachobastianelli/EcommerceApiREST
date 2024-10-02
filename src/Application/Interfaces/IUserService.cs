using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        User GetById(int id);
        List<User> GetAll();
        User Create (UserCreateRequest user);
        void Update (int id, UserUpdateRequest user);
        void Delete (int id);
    }
}
