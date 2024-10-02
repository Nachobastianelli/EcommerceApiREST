using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<User> GetById([FromRoute] int id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpGet]
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

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute]int id, [FromBody] UserUpdateRequest user) 
        {
            _service.Update(id, user);
            return NoContent();
        }


    }
}
