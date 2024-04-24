using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ZadatakV2.Dto.Models;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.Service.Abstractions;

namespace ZadatakV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {           
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly ApplicationDbContext _dbContext;


        private readonly IAuthService _authenticationService;
        public UserController(IAuthService authenticationService,                              
                              IPasswordHasher passwordHasher, 
                              IJwtProvider jwtProvider, 
                              ApplicationDbContext dbContext)
        {
            _authenticationService = authenticationService;            
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            long id = await _authenticationService.RegisterUserAscync(registerRequest);                                
            return Ok();
        }

        [HttpPost("login")]
        public async Task<LoginResponse> Login([FromBody] LoginRequest loginRequest)
        {            
            LoginResponse response = await _authenticationService.LoginAsync(loginRequest);
            return response;                                    
        }

        [HttpPost("refresh")]        
        public async Task<LoginResponse> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            LoginResponse response = await _authenticationService.RefreshTokenAsync(refreshTokenRequest);                                    

            return response;
        }

        [HttpGet("test")]
        [Authorize]
        public async Task<IActionResult> Test()
        {
            return Ok("authenticated");
        }

        [HttpGet("test2")]
        public async Task<IActionResult> Test2()
        {
            return Ok("allow anonymus");
        }
    }
}
