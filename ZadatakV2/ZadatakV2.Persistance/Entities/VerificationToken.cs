namespace ZadatakV2.Persistance.Entities
{
    public sealed class VerificationToken
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string? Token { get; set; }
        public DateTime TokenExpiryTime { get; set; }
    }
}
