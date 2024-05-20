using ZadatakV2.Shared.Interfaces;
using ZadatakV2.Shared.NewFolder;

namespace ZadatakV2.Service.Abstractions
{
    public interface IAuthService
    {
        Task RegisterUserAsync(IRegisterRequest registerRequest);
        Task<ILoginServiceResponse> LoginAsync(ILoginRequest loginRequest);
        Task LogoutAsync();
        Task<ILoginServiceResponse> RefreshTokenAsync(IRefreshTokenRequest refreshTokenRequest);
        Task VerifyEmailAsync(string token);
    }
}
