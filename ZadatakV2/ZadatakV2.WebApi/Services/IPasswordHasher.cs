namespace ZadatakV2.WebApi.Services
{
    public interface IPasswordHasher
    {
        bool VerifyPassword(string passwordHash, string inputPassword);
        string Hash(string password);

    }
}
