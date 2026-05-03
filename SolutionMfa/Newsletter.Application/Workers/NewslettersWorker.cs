using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newsletter.Core.Services.Abstraction;

namespace Newsletter.Application.Workers;

public sealed class NewslettersWorker(ILogger<NewslettersWorker> logger,
    IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private readonly TimeSpan _scheduleTime = new(8, 0, 0);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Iniciando Worker para geração da Newsletter semanal.");

        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.UtcNow;
            var nextRun = GetNextSundayAtEigth(now);
            //var delay = nextRun - now;

            var delay = TimeSpan.FromSeconds(5);
            logger.LogInformation("A próxima execução foi agendada para {NextRun}", nextRun);

            try
            {
                await Task.Delay(delay, stoppingToken);

                logger.LogInformation("Executando em Domingo em: {Time} - UTC", DateTime.UtcNow);

                await DoWorkAsync(cancellationToken: stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }

    private DateTime GetNextSundayAtEigth(DateTime current)
    {
        var daysUntilSunday = ((int)DayOfWeek.Sunday - (int)current.DayOfWeek + 7) % 7;
        var nextSunday = current.Date.AddDays(daysUntilSunday).Add(_scheduleTime);

        if (nextSunday <= current)
            nextSunday = nextSunday.AddDays(7);

        return nextSunday;
    }

    private async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var newsletterService = scope.ServiceProvider.GetRequiredService<INewsletterService>();
        await newsletterService.SendAsync(cancellationToken);
    }
}