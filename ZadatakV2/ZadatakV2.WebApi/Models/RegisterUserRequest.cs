using AutoMapper;

namespace ZadatakV2.WebApi.Models
{
    public sealed class RegisterUserRequest 
    {        
        public string Email { get; set; }
        public string Password { get; set; }        
    }
}
