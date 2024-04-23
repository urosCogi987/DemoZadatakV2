using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZadatakV2.WebApi.Entities;
using ZadatakV2.WebApi.Models;
using ZadatakV2.WebApi.Services;
using static System.Net.Mime.MediaTypeNames;

namespace ZadatakV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;        
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly ApplicationDbContext _dbContext;

        public UserController(IMapper mapper, 
                              IPasswordHasher passwordHasher, 
                              IJwtProvider jwtProvider, 
                              ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerRequest)
        {
            registerRequest.Password = _passwordHasher.Hash(registerRequest.Password);
            User user = _mapper.Map<User>(registerRequest);

            await _dbContext.Set<User>().AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {            
            User? user = await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.Email == loginRequest.Email);

            bool verified = _passwordHasher.VerifyPassword(user.Password, loginRequest.Password);

            if (verified)
                return Ok(_jwtProvider.GenerateToken(user));

            return BadRequest("Invalid credentials");
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
