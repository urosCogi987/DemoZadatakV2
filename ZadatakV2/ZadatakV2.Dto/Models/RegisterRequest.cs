namespace ZadatakV2.Dto.Models
{
    //public sealed record RegisterRequest(string Email, string Password);    
    public sealed class RegisterRequest(string email, string password)
    {
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
    }
}
