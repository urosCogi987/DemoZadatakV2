using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Dto.Models
{
    public sealed class LoginRequest : ILoginRequest
    {
        public string Email { get; set; }

        public string Password {  get; set; }
    }
}
