using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.Service.Abstractions
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}
