using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using ZadatakV2.Service.Abstractions;

namespace ZadatakV2.Service.Services
{
    public class EmailProvider(IConfiguration configuration) : IEmailProvider
    {       
        public async Task SendConfirmationEmaiAsync(string email, string token)
        {
            string url = $"https://localhost:7107/api/authentication/verify?token={token}";
            
            var client = new SendGridClient(configuration["Sendgrid:ApiKey"]);
            var from_email = new EmailAddress(configuration["Sendgrid:Verifiedsender"]);
            var subject = "VerifyEmail";
            var to_email = new EmailAddress(email);
            var plainTextContent = $"Verify email: {url}";
            var htmlContent = $"<strong>Verify email: <a href=\"{url}\">verify</strong>";
            var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, plainTextContent, htmlContent);
            
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);            
        }
    }
}
