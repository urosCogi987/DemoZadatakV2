namespace ZadatakV2.Service.Abstractions
{
    public interface IEmailProvider
    {
        Task SendConfirmationEmaiAsync(string email, string token);
    }
}
