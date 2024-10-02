using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User Create(UserCreateRequest user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(user.Surname) || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                throw new ArgumentException("All fiels must be complet!");

            var isUserExist = _repository.GetByEmail(user.Email);

            if (isUserExist != null)
                throw new Exception("No se puede repetir email"); //Cambiar excepcion en un futuro

            var usuario = new User
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Surname = user.Surname,
            };

            return _repository.Add(usuario);
        }

        public void Delete(int id)
        {
            var user = _repository.GetById(id) ?? throw new ArgumentNullException(nameof(id));

            _repository.Delete(user);
            
        }

        public List<User> GetAll()
        {
            var objs = _repository.GetAll() ?? throw new ArgumentNullException("Mejorar errores, ver video");
            return objs;
        }

        public User GetById(int id)
        {
            var obj = _repository.GetById(id) ?? throw new ArgumentNullException(nameof(GetById));
            return obj;
        }

        public void Update(int id, UserUpdateRequest user)
        {
            var usuario = _repository.GetById(id) ?? throw new NotFoundException(typeof(User).ToString(), id);

            if (user.Name != null) usuario.Name = user.Name;
            if (user.Surname != null) usuario.Surname = user.Surname;
            if (user.Password != null) usuario.Password = user.Password;

            _repository.Update(usuario);
        }
    }
}
