using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValorationController : ControllerBase
    {
        private readonly IValorationService _valorationService;
        public ValorationController(IValorationService valorationService)
        {
            _valorationService = valorationService;
        }

        [HttpPost("{productId}")]
        public ActionResult<Valoration> Create([FromRoute]int productId, [FromBody] ValorationCreateRequest valorationCreateRequest)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new NotFoundException($"Cannot found a user whit this id");
            return Ok(_valorationService.Create(productId, valorationCreateRequest, userId));
        }

        [HttpGet]
        public ActionResult<List<Valoration>> Get()
        {
            return Ok(_valorationService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Valoration> Get([FromRoute]int id) 
        {
            return Ok(_valorationService.GetById(id));
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute]int id,[FromBody] ValorationUpdateRequest valorationUpdateRequest)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new NotFoundException($"Cannot found a user whit this id");
            _valorationService.Update(id, valorationUpdateRequest, userId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            _valorationService.Delete(id);
            return NoContent();
        }
    }
}
