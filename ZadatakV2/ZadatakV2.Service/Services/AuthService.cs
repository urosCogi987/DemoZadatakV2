using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ZadatakV2.Domain.Repositories;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Service.Models.CustomModels;
using ZadatakV2.Shared.Exceptions;
using ZadatakV2.Shared.Interfaces;
using ZadatakV2.Shared.NewFolder;
using ZadatakV2.Shared.Resources;

namespace ZadatakV2.Service.Services
{
    public sealed class AuthService(IPasswordHasher passwordHasher,
                                    IUserRepository userRepository,
                                    IMapper mapper,
                                    IJwtProvider jwtProvider,
                                    IHttpContextAccessor httpContextAccessor,
                                    IEmailProvider emailProvider,
                                    IVerificationTokenRepository verificationTokenRepository) : IAuthService
    {       
        public async Task RegisterUserAsync(IRegisterRequest registerRequest)
        {
            if (!await userRepository.IsEmailUniqueAsync(registerRequest.Email))
                return;

            User user = mapper.Map<User>(registerRequest);
            user.Password = passwordHasher.Hash(registerRequest.Password);

            User addedUser = await userRepository.AddItemAsync(user);
            
            VerificationToken token = new()
            {
                Value = jwtProvider.GenerateEmptyToken(),
                TokenExpiryTime = DateTime.UtcNow.AddHours(1),
                UserId = addedUser.Id
            };

            await verificationTokenRepository.AddItemAsync(token);
            await emailProvider.SendConfirmationEmaiAsync(user.Email! ,token.Value);            
        }

        public async Task<ILoginServiceResponse> LoginAsync(ILoginRequest loginRequest)
        {           
            User? user = await userRepository.FindUserByEmailAsync(loginRequest.Email);
            if (user == null)
                throw new EntityNotFoundException(Resource.INVALID_CREDENTIALS);
            
            bool verified = passwordHasher.VerifyPassword(user.Password, loginRequest.Password);
            if (!verified)
                throw new EntityNotFoundException(Resource.INVALID_CREDENTIALS);

            if (!await userRepository.IsEmailVerified(user.Email!))
                throw new InvalidRequestException("Email is not verified");

            if (await userRepository.IsUserBlocked(user.Id))
                throw new InvalidRequestException("Your accout has been restricted from using this API.");

            string accessToken = jwtProvider.GenerateAccessToken(user);
            string refreshToken = jwtProvider.GenerateEmptyToken();

            SetRefreshToken(user, refreshToken);
            await userRepository.UpdateItemAsync(user);

            return new LoginServiceResponse { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public async Task LogoutAsync()
        {
            long id = GetUserIdFromContext();

            User? user = await userRepository.GetItemByIdAsync(id);
            if (user is null)
                throw new EntityNotFoundException("Uer is not logged in");

            DeleteRefreshToken(user);
            await userRepository.UpdateItemAsync(user);
        }

        public async Task<ILoginServiceResponse> RefreshTokenAsync(IRefreshTokenRequest refreshTokenRequest)
        {
            long id = GetUserIdFromContext();

            User? user = await userRepository.GetItemByIdAsync(id);
            if (user is null)
                throw new EntityNotFoundException("Uer is not logged in");

            if (user.RefreshToken != refreshTokenRequest.RefreshToken || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                DeleteRefreshToken(user);
                await userRepository.UpdateItemAsync(user);
                return new LoginServiceResponse();
            }

            string accessToken = jwtProvider.GenerateAccessToken(user);
            string refreshToken = jwtProvider.GenerateEmptyToken();

            SetRefreshToken(user, refreshToken);
            await userRepository.UpdateItemAsync(user);

            return new LoginServiceResponse { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public async Task VerifyEmailAsync(string token)
        {
            User? user = await verificationTokenRepository.GetUserByToken(token);                        
            if (user is null)
                throw new InvalidRequestException("Invalid token");

            user.IsEmailVerified = true;
            await userRepository.UpdateItemAsync(user);
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

        private long GetUserIdFromContext()
        {
            long id = -1;
            long.TryParse(httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out id);

            return id;
        }        
    }
}
