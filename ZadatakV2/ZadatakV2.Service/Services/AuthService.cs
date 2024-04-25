using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ZadatakV2.Domain.Repositories;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Service.Models.CustomModels;
using ZadatakV2.Shared.Interfaces;
using ZadatakV2.Shared.NewFolder;

namespace ZadatakV2.Service.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IPasswordHasher passwordHasher,
                           IUserRepository userRepository,
                           IMapper mapper,
                           IJwtProvider jwtProvider,
                           IHttpContextAccessor httpContextAccessor)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
            _httpContextAccessor = httpContextAccessor;
        }
        
        //public async Task<long> RegisterUserAscync(RegisterRequest registerRequest)
        //{
        //    registerRequest.Password = _passwordHasher.Hash(registerRequest.Password);

        //    User user = _mapper.Map<User>(registerRequest);

        //    return await _userRepository.AddUserAsync(user);            
        //}

        

        //public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        //{
        //    User? user = await _userRepository.FindUserByEmailAsync(loginRequest.Email);
        //    if (user == null)
        //        throw new Exception("Invalid credentials.");

        //    bool verified = _passwordHasher.VerifyPassword(user.Password, loginRequest.Password);
        //    if (!verified)
        //        throw new Exception("Invalid credentials.");

        //    string accessToken = _jwtProvider.GenerateAccessToken(user);
        //    string refreshToken = _jwtProvider.GenerateRefreshToken();

        //    user.SetRefreshToken(refreshToken);
        //    await _userRepository.UpdateUserAsync(user);

        //    return new(accessToken, refreshToken);
        //}

        //public async Task<ILoginResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        //{
        //    var id = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    User user = await _userRepository.FindUserByIdAsync(long.Parse(id));
        //    if (user is null)
        //        throw new Exception("Uer with that id doesnt exist");

        //    if (user.RefreshToken != refreshTokenRequest.RefreshToken)
        //    {
        //        user.DeleteRefreshToken();
        //        await _userRepository.UpdateUserAsync(user);
        //    }

        //    string accessToken = _jwtProvider.GenerateAccessToken(user);
        //    string refreshToken = _jwtProvider.GenerateRefreshToken();

        //    user.SetRefreshToken(refreshToken);
        //    await _userRepository.UpdateUserAsync(user);

        //    return new LoginResponse() { AccessToken = accessToken, RefreshToken = refreshToken };
        //}

        public async Task<long> RegisterUserAscync(IRegisterRequest registerRequest)
        {
            User user = _mapper.Map<User>(registerRequest);
            user.Password = _passwordHasher.Hash(registerRequest.Password);
            return await _userRepository.AddUserAsync(user);            
        }

        public async Task<ILoginServiceResponse> LoginAsync(ILoginRequest loginRequest)
        {
            User? user = await _userRepository.FindUserByEmailAsync(loginRequest.Email);
            if (user == null)
                throw new Exception("Invalid credentials.");

            bool verified = _passwordHasher.VerifyPassword(user.Password, loginRequest.Password);
            if (!verified)
                throw new Exception("Invalid credentials.");

            string accessToken = _jwtProvider.GenerateAccessToken(user);
            string refreshToken = _jwtProvider.GenerateRefreshToken();

            user.SetRefreshToken(refreshToken);
            await _userRepository.UpdateUserAsync(user);

            return new LoginServiceResponse { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public async Task<ILoginServiceResponse> RefreshTokenAsync(IRefreshTokenRequest refreshTokenRequest)
        {
            long id = -1;
            long.TryParse(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out id);
            
            User user = await _userRepository.FindUserByIdAsync(id);
            if (user is null)
                throw new Exception("Uer with that id doesnt exist");

            if (user.RefreshToken != refreshTokenRequest.RefreshToken || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                user.DeleteRefreshToken();
                await _userRepository.UpdateUserAsync(user);
                return new LoginServiceResponse();
            }

            string accessToken = _jwtProvider.GenerateAccessToken(user);
            string refreshToken = _jwtProvider.GenerateRefreshToken();

            user.SetRefreshToken(refreshToken);
            await _userRepository.UpdateUserAsync(user);

            return new LoginServiceResponse { AccessToken = accessToken, RefreshToken = refreshToken };
        }
    }
}
