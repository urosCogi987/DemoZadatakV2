namespace ZadatakV2.Persistance.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public void SetRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken; 
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);
        }

        public void DeleteRefreshToken()
        {
            RefreshToken = null;
            RefreshTokenExpiryTime = DateTime.MinValue;
        }
    }
}
