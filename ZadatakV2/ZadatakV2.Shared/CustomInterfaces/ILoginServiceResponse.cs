namespace ZadatakV2.Shared.NewFolder
{
    public interface ILoginServiceResponse
    {
        string AccessToken { get; }
        string RefreshToken { get; }
    }
}
