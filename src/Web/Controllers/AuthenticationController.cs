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


        /// <summary>
        /// Authenticates a user
        /// </summary>
        /// <remarks>
        /// Return a JWT token for the user logged in, with a role claim iqual to email passed in the body.
        /// </remarks>
        /// 
        [HttpPost("authenticate")]
        public ActionResult<string> Autenticar(AuthenticationRequest request)
        {
            string token = _customAuthenticationService.Autenticar(request);

            return Ok(token);
        }
    }
}
