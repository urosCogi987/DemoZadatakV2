using ZadatakV2.Dto.Models;

namespace ZadatakV2.Service.Abstractions
{
    public interface IAuthService
    {
        Task<long> RegisterUserAscync(RegisterRequest registerRequest);
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
    }
}
