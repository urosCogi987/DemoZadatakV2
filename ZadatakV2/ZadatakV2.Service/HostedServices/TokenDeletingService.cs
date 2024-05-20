using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ZadatakV2.Persistance.Abstractions;
using VerificationTokenEntity = ZadatakV2.Persistance.Entities.VerificationToken;

namespace ZadatakV2.Service.HostedServices
{
    public class TokenDeletingService(ILogger<TokenDeletingService> logger,
                                      IConfiguration configuration,
                                      IServiceProvider serviceProvider) : BackgroundService
    {
        private const int MinuteInMilliseconds = 60000;
        private const string ClassName = nameof(TokenDeletingService);
     
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int delayMinutes = int.Parse(configuration["TokenDeletingService:MinutesDelay"]!);            

            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation($"{ClassName} running: {DateTimeOffset.Now}");

                using (IServiceScope scope = serviceProvider.CreateScope())
                {
                    var verificationTokenRepository = scope.ServiceProvider.GetRequiredService<IVerificationTokenRepository>();

                    List<VerificationTokenEntity> tokens = (await verificationTokenRepository.GetItemsAsync()).ToList();
                    tokens.ForEach(token =>
                    {
                        if (token.TokenExpiryTime < DateTime.UtcNow)
                        {
                            verificationTokenRepository.DeleteItemAsync(token);
                            logger.LogInformation($"Token with id: {token.Id} deleted");
                        }
                    });
                }
                
                await Task.Delay(MinuteInMilliseconds * delayMinutes, stoppingToken);                
            }
        }    
    }
}
