using ZadatakV2.WebApi.Entities;

namespace ZadatakV2.WebApi.Services
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
