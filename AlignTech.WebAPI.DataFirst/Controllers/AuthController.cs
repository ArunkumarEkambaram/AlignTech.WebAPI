using AlignTech.WebAPI.DataFirst.DTOs;
using AlignTech.WebAPI.DataFirst.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AlignTech.WebAPI.DataFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authService.CreateUserAsync(registerDto);
            if(result == null)
            {
                return BadRequest(new { message = "Username already exists." });
            }
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            var result = await _authService.LoginAsync(userDto);
            if(result == null)
            {
                return BadRequest("Either Username or Password is incorrect");
            }
            return Ok(result);
        }
    }
}
