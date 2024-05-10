using ZadatakV2.Shared.Interfaces;
using ZadatakV2.Shared.NewFolder;

namespace ZadatakV2.Service.Abstractions
{
    public interface IAuthService
    {
        Task RegisterUserAscync(IRegisterRequest registerRequest);
        Task<ILoginServiceResponse> LoginAsync(ILoginRequest loginRequest);
        Task<ILoginServiceResponse> RefreshTokenAsync(IRefreshTokenRequest refreshTokenRequest); 
    }
}
