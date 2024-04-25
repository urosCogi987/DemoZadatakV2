using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Dto.Models
{
    public class RefreshTokenRequest : IRefreshTokenRequest
    {
        public string RefreshToken { get; set; }
    }    
}
