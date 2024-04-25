namespace ZadatakV2.Shared.Interfaces
{
    public interface ILoginResponse
    {
        string AccessToken { get; }
        string RefreshToken { get; }
    }
}
