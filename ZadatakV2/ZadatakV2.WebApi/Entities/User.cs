using AutoMapper;

namespace ZadatakV2.WebApi.Entities
{
    public class User
    {        
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
