using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userCredentials)
        {
            var result = _authService.Login(userCredentials);
            return StatusCode(result.Success ? 200 : 400, result);
        }
        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userCredentials)
        {
            var result = _authService.Register(userCredentials);
            return StatusCode(result.Success ? 200 : 400, result);
        }
    }
}
