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
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<User> GetById([FromRoute] int id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<User>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public ActionResult<UserCreateRequest> Add(UserCreateRequest user)
        {
            _service.Create(user);
            return user;
        }
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute]int id, [FromBody] UserUpdateRequest user) 
        {
            _service.Update(id, user);
            return NoContent();
        }

        [Authorize] //endpoint solo de pruba como para ver como se traen los datos del contexto
        [HttpGet("profile")]
        public IActionResult GetUserProfile()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            

            var userData = new
            {
                userId,
                email,
                role
            };

            return Ok(userData);
        }

        [Authorize]
        [HttpGet("GetUserWEmail/{email}")]
        public ActionResult<User> GetByEmail([FromRoute]string email)
        {
            return _service.GetByEmail(email);
        }


    }
}
