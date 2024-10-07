using Application.Interfaces;
using Application.Models;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomAuthenticationService _customAuthenticationService;

        public AuthenticationController(IConfiguration configuration, ICustomAuthenticationService customAuthenticationService)
        {
            _configuration = configuration;
            _customAuthenticationService = customAuthenticationService;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequest request)
        {
            try
            {
                string token = _customAuthenticationService.Autenticar(request);
                return Ok(token);
            }
            catch (NotAllowedException ex)
            {
                return BadRequest(new{message = ex.Message});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
