using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
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

        private bool checkStars(Stars star)
        {
            return ((int)star >= 1 && (int)star <= 5);
        }
        public Valoration Create(int productID, ValorationCreateRequest valoration, string userId)
        {
            var product = _productRepository.GetById(productID) ?? throw new NotFoundException($"Not product whit the id {productID} found.");
            var user = _userRepository.GetById(int.Parse(userId)) ?? throw new NotFoundException($"Not user whit the id {userId} found.");

            if (!checkStars(valoration.Stars))
                throw new ArgumentException("The stars must be between 1 and 5");

            var newValoration = new Valoration()
            {
                CreatedAt = DateTime.UtcNow,
                IdProduct = productID,
                IdUser = int.Parse(userId),
                Opinion = string.IsNullOrWhiteSpace(valoration.Opinion) ? "" : valoration.Opinion,
                Stars = valoration.Stars,
            };

            _valorationRepository.Add(newValoration);
            return newValoration;
        }

        public void Delete(int id, string userId)
        {
            var existingValoration  = _valorationRepository.GetById(id) ?? throw new NotFoundException($"Not Valoration whit the id {id} found");
            if (existingValoration.IdUser != int.Parse(userId)) throw new NotAllowedException("You cannot modify a review that is not yours.");
            _valorationRepository.Delete(existingValoration);
        }

        public List<Valoration> GetAll()
        {
            return _valorationRepository.GetAll();
        }

        public Valoration GetById(int id)
        {
            var valoration = _valorationRepository.GetById(id) ?? throw new NotFoundException($"Not Valoration whit the id {id} found");
            return valoration;
        }

        public void Update(int id, ValorationUpdateRequest valoration, string userId)
        {
            var existingValoration = _valorationRepository.GetById(id) ?? throw new NotFoundException($"Not Valoration whit the id {id} found");

            if (existingValoration.IdUser != int.Parse(userId)) throw new NotAllowedException("You cannot modify a review that is not yours."); 

            if (!string.IsNullOrWhiteSpace(valoration.Opinion))
                existingValoration.Opinion = valoration.Opinion;

            if (valoration.Stars.HasValue && valoration.Stars != existingValoration.Stars && checkStars(valoration.Stars.Value))
                existingValoration.Stars = valoration.Stars.Value;

            _valorationRepository.Update(existingValoration);

        }
    }
}
