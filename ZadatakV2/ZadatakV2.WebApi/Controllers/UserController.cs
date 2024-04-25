﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZadatakV2.Dto.Models;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Shared.Interfaces;
using ZadatakV2.Shared.NewFolder;

namespace ZadatakV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {                   
        private readonly IAuthService _authenticationService;
        private readonly IMapper _mapper;

        public UserController(IAuthService authenticationService,                              
                              IMapper mapper)
        {
            _authenticationService = authenticationService;            
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            long id = await _authenticationService.RegisterUserAscync(registerRequest);                                        
            return Ok(id);
        }

        [HttpPost("login")]
        public async Task<LoginResponse> Login([FromBody] LoginRequest loginRequest)
        {            
            ILoginServiceResponse response = await _authenticationService.LoginAsync(loginRequest);
            return _mapper.Map<LoginResponse>(response);
            
        }

        [HttpPost("refresh")]        
        public async Task<LoginResponse> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {            
            ILoginServiceResponse response = await _authenticationService.RefreshTokenAsync(refreshTokenRequest);
            return _mapper.Map<LoginResponse>(response);            
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
