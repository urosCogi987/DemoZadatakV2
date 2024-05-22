using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Transactions;
using ZadatakV2.Persistance.Abstractions;
using VerificationTokenEntity = ZadatakV2.Persistance.Entities.VerificationToken;

namespace ZadatakV2.Service.HostedServices
{
    public class TokenDeletingServiceV2(ILogger<TokenDeletingServiceV2> logger,
                                        IConfiguration configuration,
                                        IServiceProvider serviceProvider) : BackgroundService
    {
        private const int MinuteInMilliseconds = 60000;
        private const string ClassName = nameof(TokenDeletingServiceV2);

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

                    using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        foreach (var token in tokens)
                        {
                            await verificationTokenRepository.DeleteItemAsync(token);
                            logger.LogInformation($"Deleting verification token with id: {token.Id}");
                        }

                        transactionScope.Complete();
                    }                                        
                }

                await Task.Delay(MinuteInMilliseconds * delayMinutes, stoppingToken);
            }
        }
    }
}
