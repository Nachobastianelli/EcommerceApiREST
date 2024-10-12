using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<Product> Create([FromBody] ProductCreateRequest product)
        {
            return Ok(_service.Create(product));
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
        public ActionResult Update([FromRoute] int id, [FromBody] ProductUpdateRequest product)
        {
            _service.Update(id, product);

            return NoContent();
        }

        [HttpGet("[action]/{name}")]
        public ActionResult<Product> GetByName(string name)
        {
            return Ok(_service.GetByName(name));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }

        [HttpGet("[action]")]
        public ActionResult<List<Product>> GetLittleQuantity()
        {
            return Ok(_service.LittleQuantity());
        }

        [HttpPut("{id}/{quantity}")]
        public ActionResult AddQuantity([FromRoute] int id, [FromRoute] int quantity)
        {
            _service.AddQuantity(id, quantity);
            return NoContent();
        }

        [HttpGet("GetW/Valorations/{id}")]
        public ActionResult<Product> GetWValorations([FromRoute]int id)
        {
            return _service.GetProductByIdWithValorations(id);
        }

    }
}
