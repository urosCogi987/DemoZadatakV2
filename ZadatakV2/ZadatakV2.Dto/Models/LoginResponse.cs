using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Dto.Models
{
    public class LoginResponse : ILoginResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
