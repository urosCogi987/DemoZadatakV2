using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
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

                    List<VerificationTokenEntity> tokens = (await verificationTokenRepository.Find(x => x.TokenExpiryTime < DateTime.UtcNow)).ToList();
                    await verificationTokenRepository.DeleteItemsRangeAsync(tokens);
                    tokens.ForEach(x => logger.LogInformation($"Deleting verification token with id: {x.Id}"));
                }
                
                await Task.Delay(MinuteInMilliseconds * delayMinutes, stoppingToken);                
            }
        }    
    }
}
