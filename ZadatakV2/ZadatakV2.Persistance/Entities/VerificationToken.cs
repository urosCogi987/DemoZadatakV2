namespace ZadatakV2.Persistance.Entities
{
    public sealed class VerificationToken
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string? Value { get; set; }
        public DateTime TokenExpiryTime { get; set; }
        
        public User? User { get; set; }
    }
}
