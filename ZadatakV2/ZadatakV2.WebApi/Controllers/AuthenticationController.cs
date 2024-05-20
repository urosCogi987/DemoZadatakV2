using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZadatakV2.Dto.Models;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Shared.NewFolder;

namespace ZadatakV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthService authenticationService, IMapper mapper) : ControllerBase
    {                            
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            await authenticationService.RegisterUserAsync(registerRequest);                                        
            return Ok();
        }

        [HttpPost("login")]
        public async Task<LoginResponse> Login([FromBody] LoginRequest loginRequest)
        {            
            ILoginServiceResponse response = await authenticationService.LoginAsync(loginRequest);
            return mapper.Map<LoginResponse>(response);            
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await authenticationService.LogoutAsync();
            return Ok();
        }

        [HttpPost("refresh")]        
        public async Task<LoginResponse> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {            
            ILoginServiceResponse response = await authenticationService.RefreshTokenAsync(refreshTokenRequest);
            return mapper.Map<LoginResponse>(response);            
        }

        [HttpGet("verify")]
        public async Task<IActionResult> Verify([FromQuery] string token)
        {
            await authenticationService.VerifyEmailAsync(token);
            return Ok();
        }
    }
}
