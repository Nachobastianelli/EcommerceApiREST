using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ValorationService : IValorationService
    {
        private readonly IValorationRepository _valorationRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public ValorationService(IValorationRepository valorationRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            _valorationRepository = valorationRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;

        }

        public Valoration Create(int productID, ValorationCreateRequest valoration, string userId)
        {
            var newValoration = new Valoration()
            {
                CreatedAt = DateTime.UtcNow,
                IdProduct = productID,
                IdUser = int.Parse(userId),
                Opinion = valoration.Opinion,
                Stars = valoration.Stars,
            };

            _valorationRepository.Add(newValoration);
            return newValoration; //Corregir y agragar valoraciones, tanto a prod, como a user



        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Valoration> GetAll()
        {
            throw new NotImplementedException();
        }

        public Valoration GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, ValorationUpdateRequest valoration)
        {
            throw new NotImplementedException();
        }
    }
}
