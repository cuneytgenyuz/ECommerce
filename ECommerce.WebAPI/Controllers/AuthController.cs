using ECommerce.Business.Interfaces;
using ECommerce.Models.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Kullanıcı adı ve şifre ile giriş yaparak token oluşturur. username= admin password= 1
        /// </summary>
        [HttpPost("Token")]
        public IActionResult Token([FromBody] UserDto userDto)
        {
            var authResponse = _authService.Authenticate(userDto);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse); 
            }

            return Ok(authResponse);
        }
    }


}
