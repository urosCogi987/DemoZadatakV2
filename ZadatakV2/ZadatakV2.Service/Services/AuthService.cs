using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Security.Claims;
using ZadatakV2.Domain.Repositories;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Service.Models.CustomModels;
using ZadatakV2.Shared.Exceptions;
using ZadatakV2.Shared.Interfaces;
using ZadatakV2.Shared.NewFolder;
using ZadatakV2.Shared.Resources;

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
                           IHttpContextAccessor httpContextAccessor,
                           IStringLocalizer<Resource> localizer)

        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
            _httpContextAccessor = httpContextAccessor;            
        }                

        public async Task RegisterUserAscync(IRegisterRequest registerRequest)
        {
            if (!await _userRepository.IsEmailUniqueAsync(registerRequest.Email))
                throw new UniqueConstraintViolationException($"User with email: {registerRequest.Email} already exists.");

            User user = _mapper.Map<User>(registerRequest);
            user.Password = _passwordHasher.Hash(registerRequest.Password);            
            await _userRepository.AddItemAsync(user);
        }

        public async Task<ILoginServiceResponse> LoginAsync(ILoginRequest loginRequest)
        {           
            User? user = await _userRepository.FindUserByEmailAsync(loginRequest.Email);
            if (user == null)
                throw new EntityNotFoundException(Resource.INVALID_CREDENTIALS);
            
            bool verified = _passwordHasher.VerifyPassword(user.Password, loginRequest.Password);
            if (!verified)
                throw new EntityNotFoundException(Resource.INVALID_CREDENTIALS);

            string accessToken = _jwtProvider.GenerateAccessToken(user);
            string refreshToken = _jwtProvider.GenerateRefreshToken();

            SetRefreshToken(user, refreshToken);
            await _userRepository.UpdateItemAsync(user);

            return new LoginServiceResponse { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public async Task<ILoginServiceResponse> RefreshTokenAsync(IRefreshTokenRequest refreshTokenRequest)
        {
            long id = -1;
            long.TryParse(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out id);
            
            User user = await _userRepository.GetItemByIdAsync(id);
            if (user is null)
                throw new EntityNotFoundException("Uer with that id doesnt exist");

            if (user.RefreshToken != refreshTokenRequest.RefreshToken || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                DeleteRefreshToken(user);
                await _userRepository.UpdateItemAsync(user);
                return new LoginServiceResponse();
            }

            string accessToken = _jwtProvider.GenerateAccessToken(user);
            string refreshToken = _jwtProvider.GenerateRefreshToken();

            SetRefreshToken(user, refreshToken);
            await _userRepository.UpdateItemAsync(user);

            return new LoginServiceResponse { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        private void SetRefreshToken(User user, string refreshToken)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);
        }

        private void DeleteRefreshToken(User user)
        {
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.MinValue;
        }
    }
}
