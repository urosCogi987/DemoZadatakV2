namespace ZadatakV2.Persistance.Entities
{
    public sealed class User
    {
        public long Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string? VerificationToken { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsBlocked { get; set; }
    }
}
