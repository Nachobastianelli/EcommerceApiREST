using Application.Interfaces;
using Application.Models;
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
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
            return Ok(_orderService.GetById(orderId, userId));
        }

        [HttpGet]
        public ActionResult<List<Order>> Get()
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
                return Forbid();

            return Ok(_orderService.GetAll());
        }

        [HttpGet("GetAllOrdersForOneUser")]
        public ActionResult<List<Order>> GetAll()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

            return Ok(_orderService.GetAllForUser(userId));
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

        [HttpPut("UpdateOrdetToStatePending")]
        public ActionResult Update([FromBody] AddressDto address)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

            _orderService.UpdateOrderToStatePending(address, userId);

            return NoContent();
        }

        [HttpPut("ConfirmOrder/{orderId}")]
        public ActionResult UpdateToConfirm([FromRoute] int orderId)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

            _orderService.ConfirmOrder(orderId, userId);

            return NoContent();
        }

        [HttpPut("CancelOrder/{orderId}")]
        public ActionResult UpdateToCancel([FromRoute] int orderId)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

            _orderService.CancelOrder(orderId, userId);

            return NoContent();
        }



    }
}
