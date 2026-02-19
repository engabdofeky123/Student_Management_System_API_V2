using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Student_Management_System_V2.DTOs.Auth;
using Student_Management_System_V2.Services.Interfaces;

namespace Student_Management_System_V2.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService service)
        {
            _authService = service;
        }

        [HttpPost("log-in")]
        public async Task<IActionResult> LogIn(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(dto);
            var result = await _authService.LogIn(dto);
            if(result.IsAuthenticated)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(dto);
            var result = await _authService.Register(dto);
            if (result.IsAuthenticated)
                return Ok(result);
            return BadRequest(result.Message);
        }
        
    }
}
