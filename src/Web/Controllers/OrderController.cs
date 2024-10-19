using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{orderId}")]
        public ActionResult<Order> Get([FromRoute] int orderId)
        {
            return Ok(_orderService.GetById(orderId));
        }

        [HttpGet]
        public ActionResult<List<Order>> Get()
        {
            return Ok(_orderService.GetAll());
        }

        [HttpPost("{productId}")]
        public ActionResult AddOrderLine([FromRoute] int productId)
        {
            //nucna seria nulo por que pudo entrar al endpoint 

            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

            _orderService.AddToCart(userId, productId);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult RemoveAllProductInOrder()
        {

            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

            _orderService.DeleteAllOrderLines(userId);

            return NoContent();

        }

        [HttpDelete("{productId}")]
        public ActionResult RemoveOrderLine([FromRoute] int productId)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

            _orderService.RemoveToCart(userId, productId);

            return NoContent();
        }

        [HttpPut("{orderId}")]
        public ActionResult Update([FromRoute] int orderId, [FromBody] Address address) 
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";  

            _orderService.Update(orderId, address, userId);

            return NoContent();
        }

    }
}
