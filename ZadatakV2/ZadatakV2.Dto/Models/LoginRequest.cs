using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Dto.Models
{
    //public sealed record class LoginRequest(string Email, string Password);    
    public sealed class LoginRequest : ILoginRequest
    {
        public string Email { get; set; }

        public string Password {  get; set; }
    }
}
