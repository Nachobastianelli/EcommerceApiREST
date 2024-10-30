using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get([FromRoute] int id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Product> Create([FromBody] ProductCreateRequest product)
        {
            var role = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value;

            if (role == "Seller" || role == "Admin")
                return Ok(_service.Create(product));
            
            return Forbid();

        }

        [HttpGet("[action]")]
        public ActionResult<List<Product>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("[action]")]
        public ActionResult<List<Product>> GetAvailable()
        {
            return Ok(_service.ShowAvailable());
        }

        [HttpGet("[action]")]
        public ActionResult<List<Product>> GetMoreCheaper()
        {
            return Ok(_service.FilterByCheapest());
        }

        [HttpGet("[action]")]
        public ActionResult<List<Product>> GetMostExpansive()
        {
            return Ok(_service.FilterByMostExpensive());
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Update([FromRoute] int id, [FromBody] ProductUpdateRequest product)
        {
            var role = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value;

            if (role == "Seller" || role == "Admin")
            {
                _service.Update(id, product);
                return NoContent();
            }

            return Forbid();
        }

        [HttpGet("[action]/{name}")]
        public ActionResult<Product> GetByName(string name)
        {
            return Ok(_service.GetByName(name));
        }

        [HttpPut("ChangeVisibility/{id}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            var role = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value;

            if (role == "Seller" || role == "Admin")
            {
                _service.Delete(id);
                return NoContent();
            }
            return Forbid();

        }

        [HttpGet("[action]")]
        public ActionResult<List<Product>> GetLittleQuantity()
        {
            return Ok(_service.LittleQuantity());
        }

        [HttpPut("{id}/{quantity}")]
        [Authorize]
        public ActionResult AddQuantity([FromRoute] int id, [FromRoute] int quantity)
        {
            var role = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value;

            if (role != "Seller")
                return Forbid();

            _service.AddQuantity(id, quantity);
            return NoContent();
        }

        

    }
}
