﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZadatakV2.Dto.Models;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Shared.NewFolder;

namespace ZadatakV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {                   
        private readonly IAuthService _authenticationService;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthService authenticationService,                              
                                        IMapper mapper)
        {
            _authenticationService = authenticationService;            
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            await _authenticationService.RegisterUserAsync(registerRequest);                                        
            return Ok();
        }

        [HttpPost("login")]
        public async Task<LoginResponse> Login([FromBody] LoginRequest loginRequest)
        {            
            ILoginServiceResponse response = await _authenticationService.LoginAsync(loginRequest);
            return _mapper.Map<LoginResponse>(response);            
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.LogoutAsync();
            return Ok();
        }

        [HttpPost("refresh")]        
        public async Task<LoginResponse> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {            
            ILoginServiceResponse response = await _authenticationService.RefreshTokenAsync(refreshTokenRequest);
            return _mapper.Map<LoginResponse>(response);            
        }

        [HttpGet("verify")]
        public async Task<IActionResult> Verify([FromQuery] string token)
        {
            await _authenticationService.VerifyEmailAsync(token);
            return Ok();
        }
    }
}
