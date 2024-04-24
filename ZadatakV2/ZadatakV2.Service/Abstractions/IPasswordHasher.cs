namespace ZadatakV2.Service.Abstractions
{
    public interface IPasswordHasher
    {
        bool VerifyPassword(string passwordHash, string inputPassword);
        string Hash(string password);

    }
}
